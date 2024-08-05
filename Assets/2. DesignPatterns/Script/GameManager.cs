using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject;

public class GameManager : MonoBehaviour
{
    // 어떤걸 싱글톤 패턴으로 만들면 좋을까?
    // ㄴ 한가지만 고민해보면 된다 : 단일 책임 원칙에 부합하는 녀석인가?
    public static GameManager Instance { get; private set; }
    public new Light light;

    public float dayLength = 5; // 밤낮의 길이
    public bool isDay = true;

    // 옵저버 패턴 : 특정 임무를 수행하는 객체에게 상태 변화 또는 특정 이벤트의 호출 조건이 발생할 시
    // 해당 이벤트 호출이 필요한 객체들이 " 나도 상태 변하면 알려주세요."라고 등록() 해놓는 형태의 디자인 패턴이다.

    private List<Monster> monsters = new(); //구독자들

    //c#의 event는 옵저버 패턴 구현에 최적화된 구조로 만들어져 있으므로 
    //event를 활용하는 것 만으로도 옵저버 패턴을 적용했다고 볼 수 있음.
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

