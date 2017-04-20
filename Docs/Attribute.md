### Inspector

| Method Attribute  |   Comment    |
| :------------- | :------------- |
| Button           |  해당 메소드를 인스펙터에서 버튼으로 호출 할 수 있다. 파라매터가 있는 메소드는 제외    |

---
| Member Attribute |  Comment     |
| :------------- | :------------- |
| ReadOnly       | 인스펙터에서 공개 시 값을 수정 할 수 없도록 한다       |

---
```
[ReadOnly]
public int Number = 0;

[ReadOnly]
[SerializeField]
private int Id = 0;
```
