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
    # return tf.strings.split(file_path, '')[0]
    spl = tf.strings.split(file_path, '\\')

    lb = tf.strings.split(spl[5], '_')[0]
    return lb
    # lb = int(file_path.split("_")[0])
    # return lb


def get_waveform_and_label(file_path):
    label = get_label(file_path)
    audio_binary = tf.io.read_file(file_path)
    waveform = decode_audio(audio_binary)
    return waveform, label


def get_spectrogram(waveform):
    # Padding for files with less than 16000 samples
    zero_padding = tf.zeros([16000] - tf.shape(waveform), dtype=tf.float32)

    # Concatenate audio with padding so that all audio clips will be of the
    # same length
    print('waveform', waveform)
    waveform = tf.cast(waveform, tf.float32)
    equal_length = tf.concat([waveform, zero_padding], 0)
    spectrogram = tf.signal.stft(
        equal_length, frame_length=255, frame_step=128)
    spectrogram = tf.abs(spectrogram)
    return spectrogram


def get_wave(folder_path='../AudioAnalytic/datas/outPut/PrivateTest', db_filter=0, file_save='', show=False):
    # AUTOTUNE = tf.data.AUTOTUNE
    AUTOTUNE = tf.data.experimental.AUTOTUNE
    filepaths = list(tf.data.Dataset.list_files(folder_path + '/*.wav'))
    # print(filepaths)
    files_ds = tf.data.Dataset.from_tensor_slices(filepaths)
    waveform_ds = files_ds.map(get_waveform_and_label, num_parallel_calls=AUTOTUNE)
    file_error_name = []
    rows = 35
    cols = 23
    n = rows * cols
    # fig, axes = plt.subplots(rows, cols, figsize=(15, 18))
    fig, axes = plt.subplots(rows, cols, figsize=(20, 24))
    # for i, (audio, label) in enumerate(waveform_ds.take(n)):
    for i, (audio, label) in enumerate(waveform_ds.take(1)):
        if filter != 0:
            # dbs = np.where(audio > 0.2)[0]
            # lấy ra tần số > ngưỡng lọc.
            dbs = np.where(audio > db_filter)[0]
            # print(audio.shape)
            # print(dbs.shape)
            if dbs.shape[0] < audio.shape[0] / 4:
                file_error = filepaths[i].numpy()
                # print(file_error)
                spl = tf.strings.split(filepaths[i], '\\').numpy()
                # print(spl)
                info = tf.strings.split(spl[5], '_').numpy()
                # print(info)
                guid = tf.strings.split(info[3], '.').numpy()[0]
                # print(guid)
                name_img = bytes.decode(guid)
                # print(name_img)
                # file_error_name.append( bytes.decode(filepaths[i].numpy()))
                file_error_name.append(name_img)

                continue

        r = i // cols
        c = i % cols
        ax = axes[r][c]
        ax.plot(audio.numpy())
        ax.set_yticks(np.arange(-1.2, 1.2, 0.2))
        label = label.numpy().decode('utf-8')
        ax.set_title(label)

    # plt.show()
    if file_save != '':
        plt.savefig('im.png')

    for waveform, label in waveform_ds.take(1):
        label = label.numpy().decode('utf-8')
        spectrogram = get_spectrogram(waveform)

    print('Label:', label)
    print('Waveform shape:', waveform.shape)
    print('Spectrogram shape:', spectrogram.shape)
    print('Audio playback')
    display.display(display.Audio(waveform, rate=16000))
    return file_error_name


def plot_spectrogram(spectrogram, ax):
    # Convert to frequencies to log scale and transpose so that the time is
    # represented in the x-axis (columns).
    log_spec = np.log(spectrogram.T)
    height = log_spec.shape[0]
    width = log_spec.shape[1]
    X = np.linspace(0, np.size(spectrogram), num=width, dtype=int)
    Y = range(height)
    ax.pcolormesh(X, Y, log_spec)


if __name__ == '__main__':
    folder_npy = '../AudioAnalytic/datas/outPut/TrainND/'
    # train_files = '../AudioAnalytic/datas/outPut/hihi/'
    file_path = '../AudioAnalytic/datas/outPut/Test/'
    file_error = get_wave()
    print(file_error)
