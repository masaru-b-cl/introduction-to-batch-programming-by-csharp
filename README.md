バッチプログラミング入門 by C#
=====

はじめに
-----

本文書はバッチアプリケーション(コンソールアプリケーション)とはどのようなもので、どうプログラミングすればよいかを身に付けることを目的としています。対象プラットフォームは.NETで言語はC#を用います。

### 想定する読者

- C#の入門書などで基本的な文法を身に付けている
- GUIアプリケーションしか作ったことがない
- ファイル操作や基本的なデータ構造などをほとんど使ったことがない

### 本文書のゴール

- バッチアプリケーションの概念を理解する
- C#でコンソールアプリケーションを作成できる
- コンソールアプリケーションの入出力を扱える
- 順次、分岐、反復を使って自分でアルゴリズムを考え実装できる
- 配列、リスト、ディクショナリといった基本データ構造を扱える
- 基本的なファイル操作を行える

### 本文書の開発環境

- Windows 10 Pro 64bit (Fall Creators Update)
- Visual Studio 2017 Community
- .NET Framework 4.7.1 (not Core)


目次
-----

1. [バッチアプリケーションとは何か？](doc/01.md)
2. [コンソールアプリケーションを作ってみよう](doc/02.md)
3. [バブルソートを実装してみよう](doc/03.md)
4. [バッチアプリケーションにデータを渡してみよう](doc/04.md)
5. [ファイルからデータを入力してみよう](doc/05.md)
6. [ファイルにデータを出力してみよう](doc/06.md)
7. [2つのファイルを使って処理してみよう](doc/07.md)
8. [エラーに備えよう](doc/08.md)
9. [データを集計してみよう](doc/09.md)


サンプルソース
-----

各章で作成したサンプルソースは、`src`フォルダーにアップしてあります。参照するには、本リポジトリをcloneもしくはzipダウンロードして展開し、VSで各ソリューションファイルを開いてください。


参考資料
-----

- 標準入力・標準出力ってなに? - Qiita  
  https://qiita.com/angel_p_57/items/03582181e9f7a69f8168
- バブルソート - Wikipedia  
  https://ja.wikipedia.org/wiki/%E3%83%90%E3%83%96%E3%83%AB%E3%82%BD%E3%83%BC%E3%83%88
- コマンドライン引数 - C# によるプログラミング入門 | ++C++; // 未確認飛行 C  
  http://ufcpp.net/study/csharp/st_command.html
- テキストファイル - Wikipedia  
  https://ja.wikipedia.org/wiki/%E3%83%86%E3%82%AD%E3%82%B9%E3%83%88%E3%83%95%E3%82%A1%E3%82%A4%E3%83%AB
- Comma-Separated Values - Wikipedia  
  https://ja.wikipedia.org/wiki/Comma-Separated_Values
- ファイル操作 - C# によるプログラミング入門 | ++C++; // 未確認飛行 C  
  http://ufcpp.net/study/csharp/lib_file.html
- コレクション - クラスライブラリ | ++C++; // 未確認飛行 C  
  http://ufcpp.net/study/dotnet/bcl_collection.html  
- .NETの例外処理 Part.1 – とあるコンサルタントのつぶやき  
  https://blogs.msdn.microsoft.com/nakama/2008/12/29/net-part-1/
- 終了ステータス - Wikipedia  
  https://ja.wikipedia.org/wiki/%E7%B5%82%E4%BA%86%E3%82%B9%E3%83%86%E3%83%BC%E3%82%BF%E3%82%B9
- Essential .NET - .NET Core によるログ記録  
  https://msdn.microsoft.com/ja-jp/magazine/mt694089.aspx?f=255&MSPPError=-2147217396
- NLog/NLog.Extensions.Logging: NLog provider for Microsoft's logging for .NET Core class libaries and console applications  
  https://github.com/NLog/NLog.Extensions.Logging


ライセンス
-----

![クリエイティブ・コモンズ・ライセンス](https://i.creativecommons.org/l/by-sa/4.0/88x31.png)

この 作品 は [クリエイティブ・コモンズ 表示 - 継承 4.0 国際 ライセンス](http://creativecommons.org/licenses/by-sa/4.0/") の下に提供されています。