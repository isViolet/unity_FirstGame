## 对话框的创建

在原先的canvas右键创建一个panel画板，可以看到它占据了canvas整个区域。

![image-20210326190752935](https://i.loli.net/2021/03/27/gGxIoLr6XT5Ru71.png)

我们需要画板出现在最下方，且长宽缩小，进行如下设置

![image-20210326191013706](https://i.loli.net/2021/03/27/8xbgjMfvcLDRhIH.png)

此时默认为占满整个画布，我们可以调节锁定的位置

![image-20210326191143708](https://i.loli.net/2021/03/27/vcUr7VkwxJCGhYZ.png)

这里修改为固定下方后就可以调节高度和宽度了。（颜色也可以改成自己喜欢的）

![image-20210326191312150](https://i.loli.net/2021/03/27/mI74E2wubAxzkLX.png)

这是我调节好画板后的游戏场景的画面。

## 为对话框创建文本

在画板里创建text（UI->text），然后设置字体风格，大小和位置

![image-20210326191747154](https://i.loli.net/2021/03/27/Kz98LyDSQn2TGiM.png)

## 为house创建碰撞器

![image-20210326192037930](https://i.loli.net/2021/03/27/wUN8bcICvdrR1Ot.png)

同时设置触发器，可以让角色碰撞门后出现对话框

## 为house创建脚本

![image-20210326192234267](https://i.loli.net/2021/03/27/JpyPAFD9ZkKloLI.png)

同时将dialog对话框拖到脚本里，这里我的对话框叫panel

```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDialog : MonoBehaviour
{

    public GameObject enterDialog;
    
    private void OnTriggerEnter2D(Collider2D coll){
        if (coll.tag == "Player")
        {
            enterDialog.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D coll){
        if (coll.tag == "Player")
        {
            enterDialog.SetActive(false);
        }
    }
}
```

为了确保用户碰到house后发生触发，设置player的tag为Player

![image-20210326192422036](https://i.loli.net/2021/03/27/tMCL9rPlKAOy73U.png)

## 实现效果

现在就可以实现对话框的效果了，不过需要注意在一开始我们要把对话框的可视化的勾去掉

![image-20210326192832038](https://i.loli.net/2021/03/27/Ij6htBnZrGylM95.png)

## 对话框淡入淡出动画的实现

![image-20210326193102357](https://i.loli.net/2021/03/27/PhiGQUFAZ64tvMl.png)

首先创建一个animation文件，拖入到对话框组件中，这是会自动生成一个control文件。

然后我们在animator中录制自己的动画

![image-20210326193501995](https://i.loli.net/2021/03/27/2TGUCNV5X7QHgDA.png)

点击左上角红色按钮进行录制，然后这时我们就可以在时间轴上配置对话框的样式变化。录制完成后点击结束录制，效果就实现了。