### Value Observer

값의 변경이 있을 시 통지를 받을 수 있다.

---

|         Class        |                 Comment                |
|:--------------------:|:--------------------------------------:|
|  IntRectiveProperty  |  Int형 값의 변경을 통지 받을 수 있다.  |
| FloatRectiveProperty | Float형 값의 변경을 통지 받을 수 있다. |


|   Mathod  |      System.Action<T>      |
|:---------:|:--------------------------:|
| Subscribe | 통지 받을 함수를 전달한다. |

```
class Player
{
    public IntRectiveProperty CurrentHp = new IntRectiveProperty();

    void Awake()
    {
        CurrentHp.Value = 100;
    }

    public DecrementHp()
    {
        CurrentHp.Value -= 1;
    }
}

class UI
{
    //인스펙터에서 연결 했다고 가정
    publid Text InstTxtHp = null;
}


class PlayGame
{
    //인스펙터에서 연결
    public Player InstPlayer = null;
    public UI InstUI = null;

    void Start()
    {
        //hp의 값이 변경된다면 처리할 함수를 전달
        InstPlayer.CurrentHp.Subscribe((hp)=>
        {
            //변경된 hp의 값을 UI클래스의 Text에 반영
            InstUI.InstTxtHp.text = hp.ToString();
        });

        StartCoroutine(TickHp());
    }

    private IEnumerator TickHp()
    {
        for(int i = 0;i<10;i++)
        {
            yield return new WaitForSeconds(1.0f);
            //이후에 값이 변경된다면 자동적으로 Text에 변경이 이루어짐
            InstPlayer.DecrementHp();
        }
    }
}

```
