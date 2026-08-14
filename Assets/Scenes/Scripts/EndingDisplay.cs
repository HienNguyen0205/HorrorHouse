using UnityEngine;
using UnityEngine.UI;

public class EndingDisplay : MonoBehaviour
{
    [Header("UI Text Components")]
    [SerializeField] private Text endingTitleText;
    [SerializeField] private Text endingDescriptionText;

    private void Start()
    {
        int endingIndex = PlayerPrefs.GetInt("FinalEndingType", 1);
        LoreCollector.EndingType ending = (LoreCollector.EndingType)endingIndex;

        UpdateEndingUI(ending);
    }

    private void UpdateEndingUI(LoreCollector.EndingType ending)
    {
        switch (ending)
        {
            case LoreCollector.EndingType.BadEnding:
                if (endingTitleText != null)
                    endingTitleText.text = "BAD ENDING: CƠN ÁC MỘNG VĨNH HẰNG";
                if (endingDescriptionText != null)
                    endingDescriptionText.text = "Bạn đã thoát khỏi ngôi biệt thự, nhưng vì chưa tìm hiểu đủ bí mật gia tộc Von Erick, lời nguyền cổ xưa đã gieo rắc vào linh hồn bạn. Bạn nhanh chóng nhận ra mình đang dần biến thành sinh vật Ghoul tiếp theo...";
                break;

            case LoreCollector.EndingType.NormalEnding:
                if (endingTitleText != null)
                    endingTitleText.text = "NORMAL ENDING: TRỐN THOÁT TRONG HOANG MANG";
                if (endingDescriptionText != null)
                    endingDescriptionText.text = "Bạn đã trốn thoát khỏi biệt thự u uất trong đêm tối. Nhưng những bí ẩn chưa giải đáp và những tiếng thì thầm trong bóng đêm vẫn sẽ mãi mãi ám ảnh tâm trí bạn...";
                break;

            case LoreCollector.EndingType.TrueEnding:
                if (endingTitleText != null)
                    endingTitleText.text = "TRUE ENDING: SỰ THẬT KINH HOÀNG & GIẢI THOÁT";
                if (endingDescriptionText != null)
                    endingDescriptionText.text = "Thu thập đủ 8 mảnh nhật ký cổ, bạn đã khám phá trọn vẹn thảm kịch Von Erick và giải phóng các linh hồn bị giam cầm. Căn biệt thự sụp đổ trong ánh sáng, chấm dứt hoàn toàn lời nguyền vĩnh hằng!";
                break;
        }
    }
}
