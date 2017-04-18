using UnityEngine;

public struct GUIRect
{
    private Rect mRect;
    public Rect rect
    {
        get
        {
            mRect.x = mCenter.x - mSize.x * 0.5f;
            mRect.y = mCenter.y - mSize.y * 0.5f;

            mRect.size = mSize;
            return mRect;
        }
    }

    private Vector2 mCenter;
    private Vector2 mSize;

    public Vector2 center
    {
        set
        {
            mCenter = value;
        }
    }
    public Vector2 size
    {
        set
        {
            mSize = value;
        }
    }

    public GUIRect(float x, float y,float width,float height)
    {
        mCenter = new Vector2(x, y);
        mSize = new Vector2(width, height);
        mRect = Rect.zero;
    }
}
