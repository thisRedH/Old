from PIL import Image
import json

class Convert:

	# Rescale
	def Size(img: Image, x: int, y: int = -1) -> Image:
		
		if y > 0:
			_new_size = x * 2, y
		else:
			_new_size = x * 2, int(x * (img.height / img.width))		# new x, (new x * ratio) = new y

		return img.resize(_new_size)

	def Ascii(img: Image, ascii_Charset: str, x: int, y: int = -1) -> str:
		img = Convert.Size(img, x, y).convert('L')

		ascii_pixel = "".join(ascii_Charset[int(i // (255 / len(ascii_Charset))) - 1] for i in img.getdata())
		return "\n".join(ascii_pixel[i:(i + img.width)] for i in range(0, len(ascii_pixel), img.width))
