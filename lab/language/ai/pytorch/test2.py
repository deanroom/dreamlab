import torch
from torch.autograd import Variable
 
# 构造一个两层的神经网络
class TwoLayerNet(torch.nn.Module):
    def __init__(self, D_in, H, D_out):
        """
        实例话两个nn.Linear模型然后把他们赋值给成员变量
        """
        super(TwoLayerNet, self).__init__()
        self.linear1 = torch.nn.Linear(D_in, H)
        self.linear2 = torch.nn.Linear(H, D_out)
 
    def forward(self, x):
        """
        在forward中，接受一个Variable作为输入，也必须返回一个Variable作为输出
        """
        h_relu = self.linear1(x).clamp(min=0)
        y_pred = self.linear2(h_relu)
        return y_pred
 
 
# N is batch size; D_in is input dimension;
# H is hidden dimension; D_out is output dimension.
N, D_in, H, D_out = 64, 1000, 100, 10
 
# 产生两个随机张量，并将它们包在Variable中
x = Variable(torch.randn(N, D_in))
y = Variable(torch.randn(N, D_out), requires_grad=False)
 
# 实例化一个网络
model = TwoLayerNet(D_in, H, D_out)
 
#实例化一个损失函数，采用MSE
criterion = torch.nn.MSELoss(size_average=False)
#实例化一个优化方法，采用SGD的方式更新参数
#model.parameters()返回网络中所有的学习参数
optimizer = torch.optim.SGD(model.parameters(), lr=1e-4)
 
for t in range(500):
    # 前向传播
    y_pred = model(x)
 
    # 打印出每一次前向传播的损失值
    loss = criterion(y_pred, y)
    print(t, loss.data[0])
 
    # 将梯度清零，否则每次反向传播计算出的梯度会叠加
    optimizer.zero_grad()
    #反向传播
    loss.backward()
    #梯度更新
    optimizer.step()
