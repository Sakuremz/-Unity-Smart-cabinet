using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoxControl : MonoBehaviour
{

    public string BoxCode;
    public GameObject BoxCodeNum;       //���Խ�Unity�еĶ��󸳸�GameObject��;
    public bool putortake;

    // Start is called before the first frame update  ����ʱ�Զ�����
    void Start()
    {
        /*
         * �������ƻ�������.��ȡ�����ϵ����<TextMesh>().���� = ����;
         * BoxCodeNum.GetComponent<���>().���� = ����;       
        */
        BoxCodeNum.GetComponent<TextMesh>().text = BoxCode;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void BoxOpen()
    {
        //transform.Find("Door").GetComponent<Animator>().SetTrigger("Open");

        //�ҵ�������DOOR
        Transform child = transform.Find("Axis");
        //��ȡ�������ϵĶ����������
        Animator ani = child.GetComponent<Animator>();

        //�����ȡ��ʱ�ж����Ƿ��ڿ���״̬
        ani.SetTrigger("Open");
        /*
        * �ж϶����Ƿ񲥷����
        * �����ǰ����״̬����"BoxOpen"���Ҷ����Ĺ�һ��ʱ����ڵ���1����ô��ʾ�����Ѿ��������
        * ��Ϸ����. ��ȡ����Ķ���״̬��Ϣ(����).�����Ƿ���("��������") && ��Ϸ����.��ȡ����Ķ���״̬��Ϣ(����).�������Ž��ȴ��� 1
        */
        if (!ani.GetCurrentAnimatorStateInfo(0).IsName("BoxOpen") && ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            // �������Ѿ���ȫ��
            if (putortake)
            {
                itemPut();
            }
            else
            {
                itemTake();
            }
        }
    }

    //������Ʒ
    public void itemPut()
    {
        Transform item = transform.Find("Item");
        Animator putani = item.GetComponent<Animator>();
        putani.SetTrigger("Put");
        if (!putani.GetCurrentAnimatorStateInfo(0).IsName("ItemPut") && putani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            BoxClose();
        }
    }

    //ȡ����Ʒ
    public void itemTake()
    {
        Transform item = transform.Find("Item");
        Animator takeani = item.GetComponent<Animator>();

        if (item.position.z < -5)
        {
            //����û����
            Animator box = transform.Find("Axis").GetComponent<Animator>();
            box.SetTrigger("Open");
            takeani.SetTrigger("Put");
            box.SetTrigger("Close");
        }
        else
        {
            takeani.SetTrigger("Stay");
        }
        if (!takeani.GetCurrentAnimatorStateInfo(0).IsName("ItemStay") && takeani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            BoxClose();
        }

    }

    //�رչ���
    public void BoxClose()
    {
        transform.Find("Axis").GetComponent<Animator>().SetTrigger("Close");
    }
}
