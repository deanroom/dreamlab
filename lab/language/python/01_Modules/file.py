with open('/home/jd/data/medias/Nightcore - Angel With A Shotgun.mp3',"rb") as f:
    read_data = f.read(4)
    print(read_data)

with open('/home/jd/data/medias/test.txt',"rb") as f:
    read_data = f.seek(1)
    read_data = f.read(1)
    print(read_data)    