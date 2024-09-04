using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_boneThrow : MonoBehaviour
{
    // �ı��� 
    public float damage = 10.0f;
    // Ÿ�� == �÷��̾�
    public Transform target;
    //��ȣ��
    public float firingAngle = 45.0f;
    //�߷�
    public float gravity = 9.8f;

    //�ڷ�ƾ ���ð�
    WaitForSeconds ws;

    // �߻�ü ��ġ
    public Transform Projectile;

    //�ӵ�
    public float speed = 5;


    private void Awake()
    {

        //Ÿ�� (�÷��̾�)��ġ
        //�÷��̾� GameObject ����
        GameObject player = GameObject.FindGameObjectWithTag("PLAYER");
        //�÷��̾��� Transform ������Ʈ ����
        if (player != null)
        {
            target = player.transform;
        }

        //�ڷ�ƾ�� �����ð� ����
        ws = new WaitForSeconds(0.3f);

    }

    void Start()
    {
        //�߻�ü �̵� �ڷ�ƾ
        StartCoroutine(SimulateProjectile());
    }

    IEnumerator SimulateProjectile()
    {
        //���ð�
        yield return ws;
        //�߻�ü ��ġ ����
        //Projectile.position = boneTr.position + new Vector3(0, 0.0f, 0);
        //Ÿ�ٱ��� �Ÿ� ���
        float targetDis = Vector3.Distance(Projectile.position, target.position);
        
        //�ӵ� ���
        float projectile_Velocity = targetDis / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);
        //�ӵ���x,y ����
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);
        //���� �ð� ���
        float flightDuration = targetDis / Vx;
        //Ÿ�� ���� ȸ��
        Projectile.rotation = Quaternion.LookRotation(target.position - Projectile.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
            elapse_time += Time.deltaTime;
            yield return null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Projectile.position.y < 0.2f)
        {
            Destroy(gameObject);
        }
    }
}
