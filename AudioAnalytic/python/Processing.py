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
    Y = np.zeros((total_files, 2))
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
        Y[train_count, :] = np.array(multi_label(lb))
        train_count += 1

    print('\r Loaded', train_count)
    x_train, x_test, y_train, y_test = train_test_split(X, Y, test_size=0.2, random_state=100)
    print("x_train", x_train.shape)
    print("x_test", x_test.shape)
    print("y_train", y_train.shape)
    print("y_test", y_test.shape)
    return x_train, y_train, x_test, y_test


# def getlabel(fileName):
#     infos = fileName.split("_")
#     label = infos[0]
#     age = int(infos[1])
#     gender = infos[2]
#
#     l_age = np.zeros(90)
#     l_age[age] = 1
#
#     l_gender = np.zeros(3)
#     if gender == 'male':
#         l_gender[0] = 1
#     else:
#         if gender == 'female':
#             l_gender[1] = 1
#         else:
#             l_gender[2] = 1
#
#     vec = np.concatenate((l_age, l_gender), axis=None)
#     #  print(vec)
#     return vec


def Train(folder):
    x_train, y_train, x_test, y_test = load_data(folder)

    # input_shape_min = (128, 188, 1)
    # input_shape_max = (128, 235, 1)
    input_shape = (128, 188, 1)

    model = tf.keras.models.Sequential([
        # tf.keras.layers.Convolution2D(512, kernel_size=(128, 42), padding='same', name='conv3'),
        tf.keras.layers.Flatten(input_shape=input_shape),
        tf.keras.layers.Dense(512, activation=tf.nn.relu),
        tf.keras.layers.Dropout(0.2),
        tf.keras.layers.Dense(10, activation=tf.nn.softmax)
    ])

    model.compile(optimizer='adam',
                  loss='sparse_categorical_crossentropy',
                  metrics=['accuracy'])
    # load old weight
    # load_checkpoint = False
    # checkpoint_filepath = 'weights.pb'
    # EPOCHS = 5
    # if (load_checkpoint):
    #     print("Looking for previous weights...")
    #     if (isfile(checkpoint_filepath)):
    #         print ('Checkpoint file detected. Loading weights.')
    #         model.load_weights(checkpoint_filepath)
    #     else:
    #         print ('No checkpoint file detected.  Starting from scratch.')
    #         load_checkpoint = False
    # else:
    #     print('Starting from scratch (no checkpoint)')
    #     load_checkpoint = False
    # if load_checkpoint:
    #     check = ModelCheckpoint("weights.{epoch:02d}-{val_acc:.5f}.pb", monitor='val_acc', verbose=1,
    #                             save_best_only=True, save_weights_only=True, mode='auto')
    #     # trained = model.fit(X_train, Y_train, batch_size=32, nb_epoch=EPOCHS, verbose=1,
    #     #                     validation_data=(X_test, Y_test), callbacks=[check])
    #     model.fit(x_train, y_train, epochs=EPOCHS, callbacks=[check])
    # else:
    #     model.fit(x_train, y_train, epochs=EPOCHS)

    model.compile(optimizer='adam',
                  loss='sparse_categorical_crossentropy',
                  metrics=['accuracy'])

    model.fit(x_train, y_train, epochs=5)
    score = model.evaluate(x_test, y_test)
    model.save(checkpoint_filepath)
    print('score/acc')
    print(score[0], score[1])


if __name__ == '__main__':
    folder_train = '../AudioAnalytic/datas/outPut/Train/'
    folder_npy = '../AudioAnalytic/datas/outPut/TrainND/'
    # Wav2Spec(folder_train, folder_npy)
    Train(folder_npy)
