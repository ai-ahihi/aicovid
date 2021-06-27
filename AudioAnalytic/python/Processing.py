# https://github.com/tensorflow/docs/tree/master/site/en/r1/tutorials
# https://librosa.org/doc/main/auto_examples/plot_display.html

import tensorflow as tf

import librosa
import librosa.display
import os
import numpy as np
import matplotlib.pyplot as plt
import librosa
import librosa.display
from sklearn.model_selection import train_test_split
from os.path import isfile

# def Wav2Spec(fileName='../AudioAnalytic/datas/outPut/Train/0a0f2632-9a69-41e7-b074-cd5775896baa.wav', N_MFCC = 128):
#     src, sr = librosa.load(fileName, sr=24000)
#     # print(sr)
#     print(src.shape[0])
#     mfcc = librosa.feature.mfcc(y=src, n_mfcc=N_MFCC)
#     mfcc = mfcc[:, :, np.newaxis]  # chanel last
#     # mfcc = mfcc[np.newaxis,np.newaxis,:,:] # chanel first
#     print(mfcc.shape)
#     outfile = '../AudioAnalytic/datas/outPut/TrainND/0a0f2632-9a69-41e7-b074-cd5775896baa.npy'
#     np.save(outfile, mfcc)

'''
Ama thanh đã đc cắt đẹp trog folder
'''


def Wav2Spec(folder='../AudioAnalytic/datas/outPut/Train/', folder_save='../AudioAnalytic/datas/outPut/TrainND/',
             N_MFCC=256):
    files = os.listdir(folder)
    min1 = 128
    min2 = 128
    for i, f in enumerate(files):
        if i % 50 == 0:
            print('process...', i)

        fileName = folder + '/' + f
        src, sr = librosa.load(fileName, sr=24000)
        # print(sr)
        # print(src.shape[0])
        mfcc = librosa.feature.mfcc(y=src, n_mfcc=N_MFCC)
        mfcc = mfcc[:, :, np.newaxis]  # chanel last
        # # mfcc = mfcc[np.newaxis,np.newaxis,:,:] # chanel first

        outfile = folder_save + f.replace('wav', 'npy')
        np.save(outfile, mfcc)
        if mfcc.shape[0] > min1:
            min1 = mfcc.shape[0]
        if mfcc.shape[2] > min2:
            min2 = mfcc.shape[2]
    # tracking shape
    print(min1, min2)


def multi_label(result):  # makes a "one-hot" vector for each class name called
    try:
        vec = np.zeros(1)
        # vec = np.full(5,-1)
        if result == 1:
            vec[0] = 1
        return vec
    except ValueError:
        return None


def load_data(folder_data, t=0.8):
    files = os.listdir(folder_data)
    total_files = len(files)

    melgram = np.load(folder_data + '/' + files[0])
    mel_dims = melgram.shape
    # print("mel_dims: ", mel_dims)

    X = np.zeros((total_files, mel_dims[0], mel_dims[1], mel_dims[2]))
    # X = np.zeros((total_files, 128, 235, 1))
    Y = np.zeros((total_files, 1))
    print(Y)
    n_train = total_files * t
    train_count = 0

    for i, f in enumerate(files):
        audio_path = folder_data + '/' + f
        if i % 50 == 0:
            print('\r Loading', i)
        melgram = np.load(audio_path)
        X[train_count, :, :, :] = melgram
        # lable
        lb = int(f.split("_")[0])
        # y = [0, 0]
        # if lb == 1:
        #     y = [0, 1]
        # Y[train_count, :] = np.array(multi_label(lb))
        Y[train_count] = lb
        train_count += 1

    print('\r Loaded', train_count)
    x_train, x_test, y_train, y_test = train_test_split(X, Y, test_size=0.2, random_state=100)
    print("x_train", x_train.shape)
    print("x_test", x_test.shape)
    print("y_train", y_train.shape)
    print("y_test", y_test.shape)
    return x_train, y_train, x_test, y_test


if __name__ == '__main__':
    folder_train = '../AudioAnalytic/datas/outPut/Train/'
    folder_npy = '../AudioAnalytic/datas/outPut/TrainND/'
    # Wav2Spec(folder_train, folder_npy)

