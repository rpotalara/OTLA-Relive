from PIL import Image
import os

img = Image.open('extracted_MachineImgLst.bmp')
width, height = img.size
# it's 512x120.
# Original DFM says height 40, width 128.
# 512 / 128 = 4 icons.
# 120 / 40 = 3 rows?

os.makedirs('Otla.Net/Assets', exist_ok=True)
names = ["zx", "cpc", "msx", "81"]
for i in range(4):
    left = i * 128
    top = 0 # Assume first row
    right = left + 128
    bottom = top + 40
    crop = img.crop((left, top, right, bottom))
    crop.save(f'Otla.Net/Assets/machine_{names[i]}.bmp')
