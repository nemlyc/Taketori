public interface IBamboo
{
    /// <summary>
    /// スコアを返す・計算して返す。
    /// </summary>
    int CalcScore(int current);

    void AttackAction();
    BambooInfo.BambooType GetBambooType();
}
