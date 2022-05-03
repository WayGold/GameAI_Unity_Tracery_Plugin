import numpy as np
import logging
import matplotlib.pyplot as plt
import os


def runAnalysis(filepath):
    # Original Analysis
    sentiment_data = np.loadtxt(filepath, dtype=int)
    logging.info(sentiment_data)
    logging.info(sentiment_data.size)

    # Dropping all neutral sentiments
    sentiment_data = sentiment_data[sentiment_data != 2]
    logging.info(sentiment_data)
    logging.info(sentiment_data.size)

    logging.info((sentiment_data == 0).sum())
    logging.info((sentiment_data == 1).sum())
    logging.info((sentiment_data == 2).sum())
    logging.info((sentiment_data == 3).sum())
    logging.info((sentiment_data == 4).sum())

    countArr = np.array([(sentiment_data == 0).sum(), (sentiment_data == 1).sum(), (sentiment_data == 2).sum(),
                        (sentiment_data == 3).sum(), (sentiment_data == 4).sum()])
    logging.info(countArr)
    logging.info(countArr.size)

    plt.title("Negative Sentiment Distribution Chart")
    plt.xlabel("Sentiment Value (Most Negative) 0 ~ 4 (Most Positive)")
    plt.ylabel("Number of Sentences")

    plt.bar(np.array([0, 1, 2, 3, 4]), countArr, align='center')
    plt.show()


if __name__ == '__main__':
    logging.getLogger().setLevel(logging.INFO)
    runAnalysis('./sentiment_negative.txt')
