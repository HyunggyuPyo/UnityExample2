using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using UnityEngine.UI;

public class AttributeController : MonoBehaviour
{
    private void Start()
    {
        //ColorAttribute�� ���� �ʵ带 ã�´�.
        //BindingFlags : public�̰ų� private ��� ���� static�� �ƴ� ���� �Ҵ� ����� Ž��.
        BindingFlags bind = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        MonoBehaviour[] monoBehaviours = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
        foreach (MonoBehaviour monoBehaviour in monoBehaviours)
        {
            Type type = monoBehaviour.GetType();

            //Ư�� �ݷ���(�迭, ����Ʈ)���� ���ǿ� �����ϴ� ��Ҹ� �������� �� ���,
            //foreach �Ǵ� List.Find�� �ټ� ������ ������ ���ľ� ��.
            //List<FieldInfo> fields = new List<FieldInfo>(type.GetFields(bind));
            //fields.FindAll(null);

            //Linq ������ Ȱ���ϸ� �̸� ����ȭ �� �� ����.
            //1. Linq�� ���ǵ� Ȯ�� �޼��� �̿��ϴ� ���
            IEnumerable<FieldInfo> colorAttachedFields = type.GetFields(bind).Where(field => field.GetCustomAttribute<ColorAttribute>() != null); ;

            //2. Linq�� ���� �������� ����� ������ Ȱ���ϴ� ���
            colorAttachedFields = from field in type.GetFields(bind)
                     where field.HasAttribute<ColorAttribute>()
                     select field;

            foreach (FieldInfo field in colorAttachedFields)
            {
                ColorAttribute att = field.GetCustomAttribute<ColorAttribute>();
                object value = field.GetValue(monoBehaviour);

                if(value is Renderer rend)
                {
                    rend.material.color = att.color;
                }
                else if(value is Graphic graph)
                {
                    graph.color = att.color;
                }
                else
                {
                    throw new Exception("����, Color Attribute�� �߸� ���̼̳׿�");
                    //Debug.LogError("����, Color Attribute�� �߸� ���̼̳׿�");
                }
            }

            IEnumerable<FieldInfo> sizeAttachedFields = from field in type.GetFields(bind)
                                  where field.HasAttribute<SizeAttribute>()
                                  select field;

            foreach (FieldInfo field in sizeAttachedFields)
            {
                SizeAttribute att = field.GetCustomAttribute<SizeAttribute>();
                object value = field.GetValue(monoBehaviour);
                
                if(value is RectTransform rect)
                {
                    if (att.eValue == Rect.size)
                    {
                        rect.sizeDelta = att.vector3; // new Vector2(att.vector3.x, att.vector3.y);
                        print("rectTransform width, height ��ȯ");
                    }
                    else
                    {
                        rect.localScale = att.vector3;
                        print("rectTransform scale ��ȯ");
                    }
                }
                else if (value is Transform obj)
                {
                    obj.localScale = att.vector3;
                    print("transform scale ��ȯ");
                }
                else
                {
                    throw new Exception("���� ������ �������� ���ϼ̱���");
                }
            }
        }
        // ���� size ��ȯ
    }
}

//Color�� ������ �� �ִ� ������Ʈ �Ǵ� ������Ʈ�� [Color]��� ��Ʈ����Ʈ�� �ٿ��� ���� �����ϰ� ����

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)] // AllowMultiple = true => �ߺ��� ����Ұ��̳�
public class ColorAttribute : Attribute
{
    public Color color;
    //public ColorAttribute(Color color) // Attribute�� �����ڿ����� ���ͷ� Ÿ���� �Ű������� �Ҵ��� ����
    //{

    //}

    public ColorAttribute(float r=0, float g=0, float b=0, float a=1)
    {
        color = new Color(r, g, b, a);
    }

    public ColorAttribute()
    {
        color = Color.black;
    }
}

public static class AttributeHelper
{
    //Ư�� ��Ʈ����Ʈ�� ������ �ִ��� ���θ� Ȯ���ϰ� ������ �� Ȯ�� �޼��� 
    public static bool HasAttribute<T>(this MemberInfo info) where T : Attribute
    {
        return info.GetCustomAttributes(typeof(T), true).Length > 0;
    }
}

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class SizeAttribute : Attribute
{
    public Vector3 vector3;
    public Rect eValue;

    public SizeAttribute(float x = 0, float y = 0, float z = 0)
    {
        eValue = Rect.scale;
        vector3 = new Vector3(x, y, z);
    }

    public SizeAttribute(float width = 0, float height = 0)
    {
        eValue = Rect.size;
        vector3 = new Vector3(width, height, 0);
    }

    public SizeAttribute()
    {
        vector3 = Vector3.zero;
    }
}

public enum Rect
{
    size,
    scale
}