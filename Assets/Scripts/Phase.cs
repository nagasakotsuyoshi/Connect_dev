// フェイズの状態種別
// Trello のconnectボードの”もっと細分化”のリストの”ステータス”カードを参照
public enum Phase
{
    Initializing, //初期値
    Draw,    //カードを引く
    UseCard, //カードを使う、置く
    Attack,  //敵に攻撃する
    Defend,  //敵の攻撃から守る
    Decision,//勝敗の判定
    End,     //ターンを終了
    Waiting, //待つ
}