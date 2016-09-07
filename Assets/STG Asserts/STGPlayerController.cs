using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

namespace STGAsserts
{
    public class STGPlayerController : MonoBehaviour
    {
        [SerializeField]
        private float moveSpeed = 50.0f;
        [SerializeField]
        private float heightShiftingSpeed = 50.0f;

        [SerializeField]
        private float centerHeight = 100.0f;
        [SerializeField]
        private float heightPerLayer = 50.0f;

        [SerializeField]
        private int minLayer = -1;
        [SerializeField]
        private int maxLayer = 1;
        [SerializeField]
        private float heightDamping = 0.9f;

        [SerializeField]
        private float maxRoll = 60.0f;
        [SerializeField]
        private float rollDamping = 0.6f;

        [SerializeField]
        private Rect moveBorder = new Rect(-60, -80, 120, 160);

        private int currLayer = 0;
        private float targetY;
        //private Rigidbody m_Rigidbody;

        // Use this for initialization
        private void Start()
        {
            if (maxLayer < minLayer)
                maxLayer = minLayer;

            setHeightLayer(0);

//             m_Rigidbody = GetComponent<Rigidbody>();
//             m_Rigidbody.maxAngularVelocity = 0;
        }

        // Update is called once per frame
        private void Update()
        {
            // 处理控制器
            float deltaX = CrossPlatformInputManager.GetAxis("Left-Right");
            float deltaZ = CrossPlatformInputManager.GetAxis("Far-Near");
            bool moveUp = CrossPlatformInputManager.GetButtonDown("MoveUp");
            bool moveDown = CrossPlatformInputManager.GetButtonDown("MoveDown");

            // 平面移动
            Vector2 shiftPlane = new Vector2(deltaX, deltaZ);
            shiftPlane.Normalize();
            shiftPlane *= moveSpeed * Time.deltaTime;

            // 高度移动
            int targetLayer = currLayer;
            if (moveUp)
                targetLayer += 1;
            if (moveDown)
                targetLayer -= 1;
            
            setHeightLayer(targetLayer);

            // 处理高度渐变
            float height = Mathf.Lerp(transform.localPosition.y, targetY, heightDamping * Time.deltaTime);
            float deltaHeight = heightShiftingSpeed * Time.deltaTime;
            if (height - targetY > deltaHeight)
            {
                height -= deltaHeight;
            }
            else if (targetY - height > deltaHeight)
            {
                height += deltaHeight;
            }
            else
            {
                height = targetY;
            }

            // Time.deltaTime
            Move(shiftPlane, height);
            //m_Rigidbody.velocity = Vector3.zero;

            // 处理机身动画
            float rawroll = -maxRoll * deltaX;
            float oldroll = transform.localRotation.eulerAngles.z;
            if (oldroll > 180)
                oldroll -= 360;
            float roll = Mathf.Lerp(oldroll, rawroll, rollDamping * Time.deltaTime);
            transform.localRotation = Quaternion.AngleAxis(roll, new Vector3(0, 0, 1));

        }

        public void Move(Vector2 shiftPlane, float height)
        {
            Vector3 pos = transform.localPosition;

            pos.x += shiftPlane.x;
            pos.z += shiftPlane.y;
            pos.y = height;

            if (pos.x < moveBorder.xMin)
                pos.x = moveBorder.xMin;
            if (pos.x > moveBorder.xMax)
                pos.x = moveBorder.xMax;
            if (pos.z < moveBorder.yMin)
                pos.z = moveBorder.yMin;
            if (pos.z > moveBorder.yMax)
                pos.z = moveBorder.yMax;

            transform.localPosition = pos;
            //m_Rigidbody.MovePosition(pos);
        }

        public void setHeightLayer(int layer)
        {
            if (layer < minLayer)
                layer = minLayer;
            if (layer > maxLayer)
                layer = maxLayer;

            /*
            if(layer != currLayer)
            {
                print("layer " + layer);
            }
            */

            currLayer = layer;
            targetY = layer * heightPerLayer + centerHeight;
        }

    }
}
