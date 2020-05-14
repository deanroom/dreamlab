
from cae import Cae


def func():
    cae = Cae("我爱中国")
    seg_list = cae.analyze()
    print("分析结果:" + '/'.join(list(seg_list)))


if __name__ == "__main__":
    func()
