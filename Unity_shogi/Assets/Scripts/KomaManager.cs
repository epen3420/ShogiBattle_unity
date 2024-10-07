using UnityEngine;

public class KomaManager : MonoBehaviour, IGradeKoma
{
  private int komaGrade = 0;




  public void UpGradeKoma(int upGradeNum)
  {
    if (komaGrade < 6)
      komaGrade += upGradeNum;
  }

  public void DownGradeKoma(int downGradeNum)
  {
    if (komaGrade > 0)
      komaGrade -= downGradeNum;
  }
}
