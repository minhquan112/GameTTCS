using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public void hide()
    {
        gameObject.SetActive(false);
    }
    public void show()
    {
        gameObject.SetActive(true);
    }
    public void updatePos(Vector3 newPos)
    {
        transform.position = newPos;
    }
    public void moveTo(Vector3 newPos)
    {
        StartCoroutine(MoveToTarget(newPos, 0.2f));
    }

    IEnumerator MoveToTarget(Vector3 targetPos, float duration)
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = transform.position;
        while (elapsedTime < duration)
        {
            // Lerp từ vị trí ban đầu đến vị trí mục tiêu dựa trên thời gian
            transform.position = Vector3.Lerp(initialPosition, targetPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Đảm bảo GameObject ở vị trí cuối cùng là vị trí mục tiêu
        transform.position = targetPos;
    }
}
