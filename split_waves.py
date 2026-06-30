from PIL import Image
import os

img = Image.open('extracted_wavImgLst.bmp')
width, height = img.size
# It's 96x72. 4 columns (24*4=96) and 3 rows (24*3=72)?
# Or 1 column of 24 and many rows?
# ImageList bitmap is usually horizontal strips if not multi-row.
# 24x24 icons. 96/24 = 4. 72/24 = 3. 4*3 = 12 icons?
# The C++ code says: "Square", "Cubic", "SqrSin", "Shaw", "E=cte", "Delta" (6 items)
# Maybe it is 24 width and 24*6 height? No, it's 96x72.

# Let's try to crop them as 24x24.
os.makedirs('Otla.Net/Assets', exist_ok=True)
names = ["Square", "Cubic", "SqrSin", "Shaw", "Ecte", "Delta"]
for i in range(6):
    # DFM ImageList often packs them in a grid or strip.
    # 96x72 could be 4x3 grid.
    row = i // 4
    col = i % 4
    left = col * 24
    top = row * 24
    right = left + 24
    bottom = top + 24
    crop = img.crop((left, top, right, bottom))
    crop.save(f'Otla.Net/Assets/wave_{names[i]}.bmp')
