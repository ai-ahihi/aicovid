"""
https://www.tensorflow.org/tutorials/audio/simple_audio
"""
import os
import pathlib

import matplotlib.pyplot as plt
import numpy as np
import seaborn as sns
import tensorflow as tf

from tensorflow.keras.layers.experimental import preprocessing
from tensorflow.keras import layers
from tensorflow.keras import models
from IPython import display


# # Set seed for experiment reproducibility
# seed = 42
# tf.random.set_seed(seed)
# np.random.seed(seed)

def decode_audio(audio_binary):
    audio, _ = tf.audio.decode_wav(audio_binary)
    return tf.squeeze(audio, axis=-1)


def get_label(file_path):
    return tf.strings.split(file_path, '')[0]
    # return tf.strings.split(file_path, '_')[0]
    # lb = int(file_path.split("_")[0])
    # return lb


def get_waveform_and_label(file_path):
    label = get_label(file_path)
    audio_binary = tf.io.read_file(file_path)
    waveform = decode_audio(audio_binary)
    return waveform, label

def get_path(file_path):

    return file_path

if __name__ == '__main__':
    folder_npy = '../AudioAnalytic/datas/outPut/TrainND/'
    # train_files = '../AudioAnalytic/datas/outPut/hihi/'
    file_path = '../AudioAnalytic/datas/outPut/Test/'
    # AUTOTUNE = tf.data.AUTOTUNE
    AUTOTUNE = tf.data.experimental.AUTOTUNE
    filepaths = list(tf.data.Dataset.list_files('../AudioAnalytic/datas/outPut/Test/*.wav'))
    # print(filepaths)
    files_ds = tf.data.Dataset.from_tensor_slices(filepaths)
    waveform_ds = files_ds.map(get_waveform_and_label, num_parallel_calls=AUTOTUNE)

    rows = 8
    cols = 5
    n = rows * cols
    fig, axes = plt.subplots(rows, cols, figsize=(10, 12))
    for i, (audio, label) in enumerate(waveform_ds.take(n)):
        # dbs = np.where(audio > 0.2)[0]
        dbs = np.where(audio > 0.0011)[0]
        print(audio.shape)
        print(dbs.shape)
        if dbs.shape[0] < audio.shape[0]/4:
            file_error = filepaths[i].numpy()
            # print(file_error)
            spl = tf.strings.split(filepaths[i], '\\').numpy()
            # print(spl)
            info = tf.strings.split(spl[5], '_').numpy()
            # print(info)
            guid = tf.strings.split(info[3], '.').numpy()[0]
            print(guid)
            print(bytes.decode(guid))





        # r = i // cols
        # c = i % cols
        # ax = axes[r][c]
        # ax.plot(audio.numpy())
        # ax.set_yticks(np.arange(-1.2, 1.2, 0.2))
        # label = label.numpy().decode('utf-8')
        # ax.set_title(label)

    plt.show()
