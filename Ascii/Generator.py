from importlib.resources import path
import sys
from PIL import Image

#      ___   _____ ______________   ___         __     ______                           __            
#     /   | / ___// ____/  _/  _/  /   |  _____/ /_   / ____/__  ____  ___  _________ _/ /_____  _____
#    / /| | \__ \/ /    / / / /   / /| | / ___/ __/  / / __/ _ \/ __ \/ _ \/ ___/ __ `/ __/ __ \/ ___/
#   / ___ |___/ / /____/ /_/ /   / ___ |/ /  / /_   / /_/ /  __/ / / /  __/ /  / /_/ / /_/ /_/ / /    
#  /_/  |_/____/\____/___/___/  /_/  |_/_/   \__/   \____/\___/_/ /_/\___/_/   \__,_/\__/\____/_/     
#

grayscale = "#Bio>+;:-,.  "

def PathModification(_path):
    if _path.find("\"") > -1:
        _newPath = _path.replace("\"", "")
    else:
        _newPath = _path
    return _newPath


def ConvertPic_size(_sizePic, _picture, _newWith):  #_withPic, _hieghtPic,
    _hieghtPic, _withPic = _sizePic
    ratio_pic = _hieghtPic / _withPic
    new_hight = int(_newWith * ratio_pic)
    resized_pic = _picture.resize((_newWith * 2, new_hight))
    return (resized_pic)

def ConvertPic_gray(_pictureSized):
    #gray_pic = _pictureSized.convert("L")
    return (_pictureSized.convert("L"))

def ConvertPic_ascii(_pictureGray):
    pixels_data = _pictureGray.getdata()
    charachters = "".join([grayscale[pixel // 22] for pixel in pixels_data])
    #print(charachters)
    return (charachters)



def main():

    newWithPreview = 58

    _Path = input("   Picture Path:\n\n     -")

    _pathPic = PathModification(_Path)

    try:
        Picture = Image.open(_pathPic)
    except:
        print("\nWARNING: Invalid Path, no Picture found\n")
        main()
        exit()

    #Ascii_image_data = ConvertPic_ascii(ConvertPic_gray(ConvertPic_size(with_pic, hight_pic, Picture, new_with_)))
    Ascii_image_data_preview = ConvertPic_ascii(ConvertPic_gray(ConvertPic_size(Picture.size, Picture, newWithPreview)))    #_withPic, _hightPic,
    print(Ascii_image_data_preview)

def LogPrint():
    print(#Logo 
        "\n"
        "       ___   _____ ______________   ___         __     ______                           __            \n"
        "      /   | / ___// ____/  _/  _/  /   |  _____/ /_   / ____/__  ____  ___  _________ _/ /_____  _____\n"
        "     / /| | \__ \/ /    / / / /   / /| | / ___/ __/  / / __/ _ \/ __ \/ _ \/ ___/ __ `/ __/ __ \/ ___/\n"
        "    / ___ |___/ / /____/ /_/ /   / ___ |/ /  / /_   / /_/ /  __/ / / /  __/ /  / /_/ / /_/ /_/ / /    \n"
        "   /_/  |_/____/\____/___/___/  /_/  |_/_/   \__/   \____/\___/_/ /_/\___/_/   \__,_/\__/\____/_/     \n"
        "                                                                                            by RedH"
    )
    
def InfoPrint():
    print(
        "\n  ================================================"
        "\n   -This Programm changes PNG/JPG into Ascii Art-"
        "\n"
        "\n                                  v.1.22.5.2b528"
        "\n  ================================================"
        "\n\n"
    )

def start():
    LogPrint()
    InfoPrint()

# -------------------------------Running the Code-------------------------------

start()

main()