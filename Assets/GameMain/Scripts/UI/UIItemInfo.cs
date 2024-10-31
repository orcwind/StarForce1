using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
using StarForce;
using TMPro;

namespace StarForce
{
   public class UIItemInfo : MonoBehaviour
{
    [SerializeField] private Image m_IconImage;
    [SerializeField] private TMP_Text m_NameText;
    [SerializeField] private TMP_Text m_DescriptionText;
    [SerializeField] private float m_VerticalOffset = 2f;

    private RectTransform m_RectTransform;
    private Canvas m_Canvas;

    private void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
        m_Canvas = GetComponentInParent<Canvas>();

        if (m_Canvas != null)
        {
            InitializeCanvas();
        }
    }

    private void Start()
    {
        // 在Start中设置Event Camera，确保场景加载完成
        if (m_Canvas != null && Camera.main != null)
        {
            m_Canvas.worldCamera = Camera.main;
        }
    }

    private void InitializeCanvas()
    {
        // 设置Canvas属性
       // m_Canvas.renderMode = RenderMode.WorldSpace;
        
        // 获取或添加CanvasScaler组件
        var canvasScaler = m_Canvas.GetComponent<CanvasScaler>();
        if (canvasScaler == null)
        {
            canvasScaler = m_Canvas.gameObject.AddComponent<CanvasScaler>();
        }
        
        // 设置Canvas的RectTransform
        var canvasRect = m_Canvas.GetComponent<RectTransform>();
        canvasRect.sizeDelta = new Vector2(3f, 2f);
        
        // 设置Canvas的缩放
        canvasRect.localScale = new Vector3(0.01f, 0.01f, 1f);
        
        // 设置UI位置
        m_RectTransform.localPosition = new Vector3(0f, m_VerticalOffset, 0f);
        
        // 添加FaceCamera组件
        if (GetComponent<FaceCamera>() == null)
        {
            gameObject.AddComponent<FaceCamera>();
        }
    }

    public void UpdateInfo(WeaponData weaponData)
    {
        if (weaponData == null) return;
        
        m_IconImage.sprite = ItemIconLoader.LoadIcon(weaponData.WeaponName, ItemType.Weapon);
        m_NameText.text = weaponData.WeaponName;
        m_DescriptionText.text = weaponData.WeaponDescription;
    }
}

public class FaceCamera : MonoBehaviour
{
    private Transform m_CameraTransform;
    private Canvas m_Canvas;
    
    private void Start()
    {
        if (Camera.main != null)
        {
            m_CameraTransform = Camera.main.transform;
        }
        m_Canvas = GetComponentInParent<Canvas>();
    }
    
    private void LateUpdate()
    {
        if (m_CameraTransform != null)
        {
            // 计算朝向摄像机的方向
            Vector3 directionToCamera = m_CameraTransform.position - transform.position;
            directionToCamera.y = 0; // 保持Y轴不变，只在XZ平面旋转

            // 设置正确的朝向
            if (directionToCamera != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(directionToCamera);
                
                // 应用180度Y轴旋转以修正UI朝向
                transform.rotation *= Quaternion.Euler(0, 180, 0);
            }
        }
    }
}
}
