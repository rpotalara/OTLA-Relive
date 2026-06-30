import re
import binascii

def extract_bmps(filename):
    with open(filename, 'r') as f:
        content = f.read()

    # Find all ImageList-like components and their Bitmaps
    # They look like: Bitmap = { <hex data> }
    # We want to find "object ... TImageList" and then its "Bitmap = { ... }"

    matches = re.finditer(r'object (\w+): TImageList.*?Bitmap = \{(.*?)\}', content, re.DOTALL)

    for match in matches:
        obj_name = match.group(1)
        hex_data = match.group(2).replace('\n', '').replace(' ', '')

        # The hex data contains a Delphi header (usually 32 bytes) then the BMP file
        # BMP starts with '424D' ('BM')
        idx = hex_data.find('424D')
        if idx != -1:
            bmp_hex = hex_data[idx:]
            bmp_data = binascii.unhexlify(bmp_hex)
            output_name = f"extracted_{obj_name}.bmp"
            with open(output_name, 'wb') as out:
                out.write(bmp_data)
            print(f"Extracted {output_name}")
        else:
            print(f"Could not find BMP start in {obj_name}")

if __name__ == "__main__":
    extract_bmps('mainForm.dfm')
