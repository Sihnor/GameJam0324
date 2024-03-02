using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVOffsetter : MonoBehaviour
{
    public float m_offsetSpeed = 0.01f;
    private Material m_material;
    private Transform m_player;
    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").transform;
        m_material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        m_material.SetTextureOffset("_BaseMap", new Vector2(-m_player.position.x, -m_player.position.z) * m_offsetSpeed);
    }

    private void LateUpdate()
    {
        this.transform.position = new Vector3(m_player.position.x, transform.position.y, m_player.position.z);
    }
}
