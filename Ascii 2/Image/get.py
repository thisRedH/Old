import os
import requests as req
import tempfile as tmpf
from PIL import Image


def get_Img(file_path: str) -> Image:
	"""
	Get a image object

	Args:
		file_path: URL or System Path

	Raises:
		ConnectionError: For a HTTP/S Response Code that is not 200
		FileNotFoundError: For a Path that could not be found

	Returns:
		Image: PIL/Pillow Image Object
	"""


	# Cut " " off if windows fcking copys it °-°
	if file_path.startswith('\"'):
		file_path = file_path[1:]
	if file_path.endswith('\"'):
		file_path = file_path[:len(file_path) - 1]


	# Download the img from the Inet (over HTTP)
	if file_path.find("http") == 0:
		r = req.get(file_path)
		if r.status_code == 200:
			with tmpf.TemporaryFile() as f:
				f.write(r.content)
				img = Image.open(f, 'r')
				img.load()
		else:
			raise ConnectionError(r.status_code)

	# Get the img elsewhere
	else:
		if os.path.exists(file_path):
			# Open the pic
			img = Image.open(file_path, 'r')
			img.load()
		else:
			raise FileNotFoundError(file_path)

	return img