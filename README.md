# 研究概要

# 実験方法(データ取得方法)
## 使用したもの
### カメラ
- 一眼レフカメラ（Canon）
  - 有効画素数 1800万画素
  - 撮像素子形式　CMOSセンサー 
- ハイパースペクトルカメラ（SPECIM製）
  - 波長域 400-1000nm
  - 波長分解能 10nm
  - スリット幅 80um

### そのほか
- 温湿度計（Inkbird 製 IBS-TH1 PLUS）
- かぼちゃ（種子：えびす）
- 防除
  - 家庭園芸用カリグリーン
  - 殺菌剤STダコニール 
 
## 撮影
#### 苗
- 北大温室：9-11月で撮影予定（去年も9-11月）
  - 苗に番号を付けて時間軸を追う
  - 週に一度防除をする 
  - RGB：苗とグレーペーパーを一緒に撮影
  - ハイパー：RGBと同じ
#### 圃場
- 北大畑：6月後半から撮影？
- 北農研：北大畑より2週間早く植えている
- 圃場での撮影
  - RGB：静止画と動画の両方
  - ハイパー：ガチ圃場は難しいので苗を並べた仮圃場を作り押し車にハイパーを取り付けて走らせる？  

## 葉っぱのクラス分類
- Health      : 撮影日に病害を視認できず、か
つ実験終了日まで病害を視認できない葉
- Pre-disease : 撮影日に病害を視認できないが，次回の撮影日には病害を視認できる葉
- Disease     : 撮影日に病害を確認できる葉

# 実験方法（データ解析）
## データの前処理
### RGB
- 対象の葉のみを切り抜く（背景が邪魔になるため）

### ハイパースペクトル
- データの選別
  - 1-9,51-60のバンドを除外（輝度値が非常に小さく不要）
- データの補正
  - 照度の違いやカメラの明るさ調節機能によりスペクトルデータにばらつきが見られる
  - 標準化：スペクトルの平均値を０，標準偏差を１にする
  - グレーペーパー補正＆標準化：スペクトル値をグレーペーパーの輝度値で除算（グレーペーパー基準にする）
- 次元削減
  - 過学習を防ぐために教師なし学習で主成分分析を行い次元数を減らす
  - 温度データ：3次元，湿度データ5次元（中村さん）
  - スペクトルデータは10次元を採用（中村さん）

### データセットの作成
- 学習，検証データで異なる日にちにするように4通りにデータセットを分けた（中村さん）
 
## 判別モデルの作成
### RGB
- 植物領域のみの画像と対象の葉のみの画像を用意
- Heatlh，Prediseaseの2クラス分類
- CNNでモデル作成
- 中村さんはNewralNetworkConsoleでやった

### ハイパースペクトル
- オートエンコーダ
  - 教師なし学習：異常検知に適したモデル
- CNN
  - 特定のバンドで疑似RGB画像を作成しCNNに突っ込む
- 葉内平均スペクトルの多変量解析
  - 葉の平均スペクトルを取り，多変量解析を行った
  - SVM，重回帰分析，PLS判別，ランダムフォレスト，多層パーセプトロン

# 結果と考察
WIP

# 今年の予定
  
# その他ファイル等

## スプレッドシート

prediseaseとhealth，どの葉っぱを使用したか管理<br>
[RGB葉画像集計表](https://docs.google.com/spreadsheets/d/1tbNKAV7CB1danPvVfIgWWCt4ZDSwH8v4kQqMqWD5iSs/edit#gid=0)

撮影の日付と撮影した苗などの管理<br>
[2021カボチャ実験ラベルメモ 中村さん修論](https://docs.google.com/spreadsheets/d/1A35fo5pXOAmIu4tNjp6b3REp1l8pscE4r0eMGdCGqJQ/edit#gid=0)

[2020カボチャ温湿度 たぶん使わない](https://docs.google.com/spreadsheets/d/1UVVwuiF20nDGDa4w5a48dML68_E-bIod-TwXK8q-52s/edit#gid=0)

[2020カボチャ撮影カレンダー たぶん使わない](https://docs.google.com/spreadsheets/d/1uWR2foyTC3ZjNgtb7cK55GopVFKTn20mrD3ev14XPso/edit#gid=0)

## Colab

ハイパーのデータセット作るやつ：中村さん<br>
[createDataSetForHyper](https://colab.research.google.com/drive/1dSaaOZigCdl3nb-4byRTb4THE6aBM8wY?hl=ja)

パラメータ調整するやつ：中村さん<br>
[MachineLearning_パラメータ調整](https://colab.research.google.com/drive/1qPUF7j6s1BdnbcI4H3NOcFfxRyeBaAcU?usp=sharing)

わいが作った色々機械学習してるやつ<br>
[createDataSet_and_machinelearning](https://colab.research.google.com/drive/14YXSF-pd3QE7KFa7iMkX6nj1nJUEW06_?hl=ja)
