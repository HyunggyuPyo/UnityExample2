using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MyProject
{
    public class AsyncTest : MonoBehaviour
    {
        //async void Start()
        //{
        //    //await WaitRandom();
        //    WaitRandom();
        //    print("WaitRandom 호출함");
        //    await Wait3Seconds();
        //    print("wait3Seconds 호출함");
        //    int a = await waitRandomAndReturn();
        //    print($"{a} 초 WaitRandomAndReturn 호출");

        //    await WaitAndCallback(() => print("니가 먼저 호출되나"));
        //    print("내가 먼저 호출되나");
        //}

        private void Start()
        {
            //async가 아닌데도 Wait3Seconds()가 끝난 후에 무언가 해야 할 경우
            Wait3Seconds().ContinueWith((Task result) =>
            {
                if(result.IsCanceled || result.IsFaulted) { print("task 실패"); }
                else if(result.IsCompleted) { print("task 성공"); }
                print("3초후");
            });
        }

        // 1. void를 반환하는 async함수 : 함수 자체는 대기 가능이나, 다른 ㅎ마수에서 대기 가능 형식으로 호출이 불가능하다.
        async void WaitRandom()
        {
            print($"대기 시작{Time.time}");
            await Task.Delay(Random.Range(1000, 2000));
            print($"대기 종료{Time.time}");
        }

        // 2. Task를 반환하는 async 함수 : 함수 자체도 대기 가능이며, 다른 대기 가능 함수에서 대기 형식으로 호출이 가능.
        // ㄴ return이 없어도 알아서 프로세스 Task로 묶어 반환함.
        async Task Wait3Seconds()
        {
            print($"3초 대시 시작 {Time.time}");
            await Task.Delay(3000);
            print($"3초 대기 종료 {Time.time}");
        }

        // 3. Tast<T>를 반환하는 async 함수 : 대기 가능 함수인건 Task를 반환하는 함수와 같으나, T return이 있어야만 함.
        async Task<int> waitRandomAndReturn()
        {
            int delay = Random.Range(1000, 2000);
            print($"{(float)delay/1000} 초 대기 시작 {Time.time}");
            await Task.Delay(delay);
            print($"{(float)delay / 1000} 초 대기 종료 {Time.time}");
            return 0;
        }

        async Task WaitAndCallback(Action callback)
        {
            await Task.Delay(1000);
            callback?.Invoke();
        }
    }
}
