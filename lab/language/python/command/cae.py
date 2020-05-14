# encoding=utf-8
import jieba


class Cae:
    def __init__(self, input):
        self.input = input

    def analyze(self, input):
        return jieba.cut(input)

    def analyze(self):
        return jieba.cut(self.input)
