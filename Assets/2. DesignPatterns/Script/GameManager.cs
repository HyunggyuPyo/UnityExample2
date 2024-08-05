using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject;

public class GameManager : MonoBehaviour
{
    // ��� �̱��� �������� ����� ������?
    // �� �Ѱ����� ����غ��� �ȴ� : ���� å�� ��Ģ�� �����ϴ� �༮�ΰ�?
    public static GameManager Instance { get; private set; }
    public new Light light;

    public float dayLength = 5; // �㳷�� ����
    public bool isDay = true;

    // ������ ���� : Ư�� �ӹ��� �����ϴ� ��ü���� ���� ��ȭ �Ǵ� Ư�� �̺�Ʈ�� ȣ�� ������ �߻��� ��
    // �ش� �̺�Ʈ ȣ���� �ʿ��� ��ü���� " ���� ���� ���ϸ� �˷��ּ���."��� ���() �س��� ������ ������ �����̴�.

    private List<Monster> monsters = new(); //�����ڵ�

    //c#�� event�� ������ ���� ������ ����ȭ�� ������ ������� �����Ƿ� 
    //event�� Ȱ���ϴ� �� �����ε� ������ ������ �����ߴٰ� �� �� ����.
    public event Action<bool> onDayNightChange;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    private float dayTemp;

    void Update()
    {
        if(Time.time - dayTemp > dayLength)
        {
            dayTemp = Time.time;
            isDay = !isDay;
            light.gameObject.SetActive(isDay);

            //foreach (Monster monster in monsters)
            //{
            //    monster.OnDayNightChange(isDay);
            //}
            onDayNightChange?.Invoke(isDay);
        }
    }

    public void OnMonsterSpawn(Monster monster)
    {
        monsters.Add(monster);
        monster.OnDayNightChange(isDay);
    }

    public void OnMonsterDespawn(Monster monster)
    {
        monsters.Remove(monster);
    }
}

