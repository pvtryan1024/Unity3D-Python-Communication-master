#
#   Hello World server in Python
#   Binds REP socket to tcp://*:5555
#   Expects b"Hello" from client, replies with b"World"
#
import os
import io
import PIL.Image as Image
import time
import zmq
import cv2
import numpy as np

context = zmq.Context()
socket = context.socket(zmq.REP)
socket.bind("tcp://*:5555")

def ImportByteArrayFromUnity(messageStream):
    img = Image.frombytes("RGBA", (640, 640), messageStream, "raw")
    # img = img.transpose(Image.FLIP_LEFT_RIGHT)
    img = img.transpose(Image.FLIP_TOP_BOTTOM)
    return img

try:
    while True:
        #  Wait for next request from client
        message = socket.recv()
        # print("Received request: %s" % message)
        print("Received request:")

        #  Do some 'work'.
        #  Try reducing sleep time to 0.01 to see how blazingly fast it communicates
        #  In the real world usage, you just need to replace time.sleep() with
        #  whatever work you want python to do, maybe a machine learning task?

        
        imgpil =  ImportByteArrayFromUnity(message)
        open_cv_image = np.array(imgpil) 
        # Convert RGB to BGR 
        open_cv_image = cv2.cvtColor(open_cv_image, cv2.COLOR_RGB2BGR) 
        cv2.imshow('imported image',open_cv_image)
        cv2.waitKey(1)

        # time.sleep(1)
        # img.show()
        
        #  Send reply back to client
        #  In the real world usage, after you finish your work, send your output here
        socket.send(b"World")
except KeyboardInterrupt:
    pass