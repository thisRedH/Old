import requests as req
from PIL import Image
import json
from datetime import datetime

from Image.get import *
from Image.convert import Convert

def init():
	# JSON
	with open("config.json") as f:
		config = json.load(f)

	return config

if __name__ == "__main__":

	config = init()

	#while True:
	#try:

	file_path: str = input("Image: ")
	img: Image = get_Img(file_path)

	print(Convert.Ascii(img, config["PreviewCharset"], 75))

	if config["Reverse"] == False:
		img_ascii: str = Convert.Ascii(img, config["Charset"], 500)
	else:
		img_ascii: str = Convert.Ascii(img, config["Charset"][::-1], 500)

	with open(f"Ascii_{datetime.now().strftime('%Y_%m_%d_%H_%M_%S')}.txt", 'w') as f:
		f.write(img_ascii)
	
	img.close()

	#except Exception as e:
	#	print(e)


	input()