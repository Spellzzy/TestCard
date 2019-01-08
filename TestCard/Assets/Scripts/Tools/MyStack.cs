using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyStack<T> : IEnumerable where T :MonoBehaviour {

    // 缓存数组
    private T[] mStackArray;
    // 初始默认长度
    private const int kDefaultArrayLength = 5;

    public int Size { get; private set; }

    public MyStack()
    {
        mStackArray = new T[kDefaultArrayLength];
        Size = 0;
    }

    public bool IsEmpty()
    {
        return Size == 0;
    }

    public void Clear()
    {
        Size = 0;
        ArrayGC(ref mStackArray, Size);
    }

    /// <summary>
    /// 出栈
    /// </summary>
    /// <returns></returns>
    public T Pop()
    {
        if (IsEmpty())
        {
            return default(T);
        }

        Size--;
        var result = mStackArray[Size];
        ArrayGC(ref mStackArray, Size);
        return result;
    }

    /// <summary>
    /// 入栈
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public T Push(T item)
    {
        if (Equals(item, default(T)))
        {
            return item;
        }
        mStackArray[Size++] = item;
        if (Size >= mStackArray.Length - 1)
        {
            Resize();
        }

        return default(T);
    }

    public T Peek()
    {
        return IsEmpty() ? default(T) : mStackArray[Size - 1];
    }

    public bool Remove(T item)
    {
        if (Equals(item, default(T)))
        {
            return false;
        }

        var isFound = false;
        for (int i = 0; i < Size; i++)
        {
            if (!isFound && Equals(item, mStackArray[i]))
            {
                isFound = true;
            }
            if (isFound)
            {
                if (i + 1 == Size)
                {
                    // 某尾处
                    Size--;
                    ArrayGC(ref mStackArray, Size);
                    return true;
                }
                // 非末尾 把其之后 元素前移
                mStackArray[i] = mStackArray[i + 1];
            }
        }

        return isFound;
    }

    /// <summary>
    /// 返回栈中的所有元素 按序列出
    /// </summary>
    /// <returns></returns>
    public IEnumerator GetEnumerator()
    {
        for (int i = Size - 1; i >=0 ; i++)
        {
            yield return mStackArray[i];
        }
    }

    /// <summary>
    /// 对当前数组扩容
    /// </summary>
    private void Resize()
    {
        var newArray = new T[mStackArray.Length * 2];
        for (int i = 0; i < mStackArray.Length; i++)
        {
            newArray[i] = mStackArray[i];
        }

        mStackArray = newArray;
    }

    private void ArrayGC(ref T[] array, int index)
    {
        for (int i = index; i < array.Length; i++)
        {
            if (Equals(default(T), array[i]))
            {
                return;
            }

            array[i] = default(T);
        }
    }

}
