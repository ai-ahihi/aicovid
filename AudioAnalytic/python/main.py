
import os
import pathlib

import matplotlib.pyplot as plt
import numpy as np
import seaborn as sns
import tensorflow as tf

from tensorflow import keras
from tensorflow.keras.layers.experimental import preprocessing
# from tensorflow.keras.experimental import preprocessing

from tensorflow.keras import layers
from tensorflow.keras import models
from IPython import display
from Processing import load_data

def _training(folder_data):
    x_train, y_train, x_test, y_test = load_data(folder_data)
    input_shape = (128, 188, 1)

    model = models.Sequential([
        layers.Input(shape=input_shape),
        # preprocessing.Resizing(32, 32),
        # norm_layer,
        layers.Conv2D(32, 3, activation='relu'),
        layers.Conv2D(64, 3, activation='relu'),
        layers.MaxPooling2D(),
        layers.Dropout(0.25),
        layers.Flatten(),
        layers.Dense(128, activation='relu'),
        layers.Dropout(0.5),
        layers.Dense(2),
    ])

    model.summary()

    model.compile(
        optimizer=tf.keras.optimizers.Adam(),
        loss=tf.keras.losses.SparseCategoricalCrossentropy(from_logits=True),
        metrics=['accuracy']
    )

    # model.fit(x_train, y_train, epochs=5)
    trained = model.fit(x_train, y_train, batch_size=32, epochs=5, verbose=1, validation_data=(x_test, y_test),
                        # callbacks=[check]
                        )
    score = model.evaluate(x_test, y_test)
    model.save("cc.h5")
    print('score/acc')
    print(score[0], score[1])


if __name__ == '__main__':
    # folder_npy = '../AudioAnalytic/datas/outPut/TrainND/'
    # _training(folder_npy)
