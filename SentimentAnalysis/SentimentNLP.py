import os
import logging
import json
from stanfordcorenlp import StanfordCoreNLP
from collections import defaultdict


class StanfordNLP:
    def __init__(self, host='http://localhost', port=9000):
        self.nlp = StanfordCoreNLP(host, port=port,
                                   timeout=30000)  # , quiet=False, logging_level=logging.DEBUG)
        self.props = {
            'annotators': 'sentiment,tokenize,ssplit,pos,lemma,ner,parse,depparse,dcoref,relation',
            'pipelineLanguage': 'en',
            'outputFormat': 'json'
        }

    def word_tokenize(self, sentence):
        return self.nlp.word_tokenize(sentence)

    def pos(self, sentence):
        return self.nlp.pos_tag(sentence)

    def ner(self, sentence):
        return self.nlp.ner(sentence)

    def parse(self, sentence):
        return self.nlp.parse(sentence)

    def dependency_parse(self, sentence):
        return self.nlp.dependency_parse(sentence)

    def annotate(self, sentence):
        return json.loads(self.nlp.annotate(sentence, properties=self.props))

    @staticmethod
    def tokens_to_dict(_tokens):
        tokens = defaultdict(dict)
        for token in _tokens:
            tokens[int(token['index'])] = {
                'word': token['word'],
                'lemma': token['lemma'],
                'pos': token['pos'],
                'ner': token['ner']
            }
        return tokens


def readFile(filepath):
    file = open(filepath, 'r')
    return file.readlines()


def process_lines(lines, sNLP):
    sentiment_list = []

    for line in lines:
        logging.info('Processing: ' + line)

        result = sNLP.annotate(line)

        for s in result["sentences"]:
            sentiment_list.append(s["sentimentValue"])

    if os.path.exists('./sentiment_positive.txt'):
        os.remove('./sentiment_positive.txt')

    sentiFile = open('./sentiment_positive.txt', 'w+')
    for value in sentiment_list:
        sentiFile.write(value + '\n')


if __name__ == '__main__':
    NLP = StanfordNLP()
    process_lines(readFile('./sentences_positive.txt'), NLP)

# See PyCharm help at https://www.jetbrains.com/help/pycharm/
