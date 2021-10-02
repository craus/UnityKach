using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singletone<GameManager>
{
    public WorldView worldView;

    public void ChangeX(MyStruct v) {
        v.x = 5;
    }

    public void ChangeFoo(MyStruct v) {
        v.foo = new Foo();
    }

    public MyStruct DoNotChangeFoo(MyStruct v) {
        return v;
    }

    [ContextMenu("Test")]
    public void Test() {
        MyStruct ms = new MyStruct();

        Debug.Log($"ms.x = {ms.x}");  // 0?

        ms.x = 4;
        Debug.Log($"ms.x = {ms.x}");  // 4?

        ChangeX(ms);
        Debug.Log($"ms.x = {ms.x}");  // 5? 4?

        Action<MyStruct> lambda = (s) => s.x = 7;
        lambda(ms);
        Debug.Log($"ms.x = {ms.x}");  // 7? 5? 4?

        ms.ChangeMyX();
        Debug.Log($"ms.x = {ms.x}");  // 11? 7? 5? 4?

        var mc = new MyClass();
        Debug.Log($"mc.F() = {mc.F()}"); // 0?

        var mcst = mc.St;

        mcst.x = 14;

        Debug.Log($"mc.F() = {mc.F()}"); // 0?

        //mc.St.x = 15;

        var oldFoo = ms.foo;

        ChangeFoo(ms);

        Debug.Log($"mc.F() = {mc.F()}"); // 0?

    }
}

public struct MyStruct
{
    public int x;

    public Foo foo;

    public MyStruct(string name) : this() {
        foo = new Foo();
        x = -19;
    }
}

public static class ExtMyStruct
{
    public static void ChangeMyX(this MyStruct v) {
        v.x = 11;
    }
}

public class MyClass
{
    private MyStruct st;

    public MyStruct St {
        get
        {
            return st;
        }
    }

    public void ChangeStX() {
        st.x = 82;
    }

    public int F() {
        return st.x;
    }
}

public class Foo
{
}