using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiraDictionary : MonoBehaviour
{// ひらがなSE取得用分割
    public Dictionary<string, int> dic = new Dictionary<string, int>() {
        {"あ", 0},{"い", 1},{"う", 2},{"え", 3},{"お", 4},
        {"か", 5},{"き", 6},{"く", 7},{"け", 8},{"こ", 9},
        {"さ", 10},{"し", 11},{"す", 12},{"せ", 13},{"そ", 14},
        {"た", 15},{"ち", 16},{"つ", 17},{"て", 18},{"と", 19},
        {"な", 20},{"に", 21},{"ぬ", 22},{"ね", 23},{"の", 24},
        {"は", 25},{"ひ", 26},{"ふ", 27},{"へ", 28},{"ほ", 29},
        {"ま", 30},{"み", 31},{"む", 32},{"め", 33},{"も", 34},
        {"や", 35},{"ゆ", 36},{"よ", 37},
        {"ら", 38},{"り", 39},{"る", 40},{"れ", 41},{"ろ", 42},
        {"わ", 43},{"を", 44},{"ん", 45},
        {"が", 46},{"ぎ", 47},{"ぐ", 48},{"げ", 49},{"ご", 50},
        {"ざ", 51},{"じ", 52},{"ず", 53},{"ぜ", 54},{"ぞ", 55},
        {"だ", 56},{"ぢ", 57},{"づ", 58},{"で", 59},{"ど", 60},
        {"ば", 61},{"び", 62},{"ぶ", 63},{"べ", 64},{"ぼ", 65},
        {"ぱ", 66},{"ぴ", 67},{"ぷ", 68},{"ぺ", 69},{"ぽ", 70},
        
        //イレギュラーな対応
        {"っぷ", 68},{"っく", 7},{"っけ", 8},{"っち", 16},{"っふ", 27},

        //音声差し替え対応あり
        {"れー", 41},{"ちゃー",77},{"らー", 38},{"ごー", 50},{"ばー", 61},


        {"きゃ",71},{"きゅ",72},{"きょ",73},
        {"しゃ",74},{"しゅ",75},{"しょ",76},
        {"ちゃ",77},{"ちゅ",78},{"ちょ",79},
        {"にゃ",80},{"にゅ",81},{"にょ",82},
        {"ひゃ",83},{"ひゅ",84},{"ひょ",85},
        {"みゃ",86},{"みゅ",87},{"みょ",88},
        {"りゃ",89},{"りゅ",90},{"りょ",91},
        {"ぎゃ",92},{"ぎゅ",93},{"ぎょ",94},
        {"じゃ",95},{"じゅ",96},{"じょ",97},
        {"ぢゃ",98},{"ぢゅ",99},{"ぢょ",100},
        {"びゃ",101},{"びゅ",102},{"びょ",103},
        {"ぴゃ",104},{"ぴゅ",105},{"ぴょ",106},
        {"ヴぁ",107},{"ヴぃ",108},{"ヴ",107},{"ヴぇ",108},{"ヴぉ",109},
       
    };
    // 訓令→ヘボン式へ変換
    public Dictionary<string,string> dicHebon = new Dictionary<string,string>()
    {
        //大文字
        {"SI","SHI"}, {"TI","CHI"},{"TU","TSU"}, {"HU","FU"},
        {"ZI","JI"}, {"ZYA","JA"}, {"ZYU","JU"},{"ZYO","JO"}, 
        {"SYA","SHA"},{"SYU","SHU"}, {"SYO","SHO"},
        {"TYA","CHA"}, {"TYU","CHU"},{"TYO","CHO"},

        //小文字
        {"si","shi"}, {"ti","chi"},{"tu","tsu"}, {"hu","fu"},
        {"zi","ji"},{"zya","ja"}, {"zyu","ju"}, {"zyo","jo"}, 
        {"sya","sha"},{"syu","shu"}, {"syo","sho"},
        {"tya","cha"}, {"tyu","chu"},{"tyo","cho"},
    };
    //大文字
    public Dictionary<string, string> dicT = new Dictionary<string, string>() {
        {"A", "A"},{"I","I"},{"U", "U"},{"E", "E"},{"O", "O"},
        {"KA", "KA"},{"KI", "KI"},{"KU", "KU"},{"KE", "KE"},{"KO", "KO"},
        {"SA", "SA"},{"SI", "SHI"},{"SU", "SU"},{"SE", "SE"},{"SO", "SO"},
        {"TA", "TA"},{"TI", "CHI"},{"TU", "TSU"},{"TE", "TE"},{"TO", "TO"},
        {"NA", "NA"},{"NI", "NI"},{"NU", "NU"},{"NE", "NE"},{"NO", "NO"},
        {"HA", "HA"},{"HI", "HI"},{"HU", "FU"},{"HE", "HE"},{"HO", "HO"},
        {"MA", "MA"},{"MI", "MI"},{"MU", "MU"},{"ME", "ME"},{"MO", "MO"},
        {"YA", "YA"},{"YU", "YU"},{"YO", "YO"},
        {"RA", "RA"},{"RI", "RI"},{"RU", "RU"},{"RE", "RE"},{"RO", "RO"},
        {"WA", "WA"},{"WO", "WO"},{"N", "N"},
        {"GA", "GA"},{"GI", "GI"},{"GU", "GU"},{"GE", "GE"},{"GO", "GO"},
        {"ZA", "ZA"},{"ZI", "JI"},{"ZU", "ZU"},{"ZE", "ZE"},{"ZO", "ZO"},
        {"DA", "DA"},{"DI", "DI"},{"DU", "DU"},{"DE", "DE"},{"DO", "DO"},
        {"BA", "BA"},{"BI", "BI"},{"BU", "BU"},{"BE", "BE"},{"BO", "BO"},
        {"PA", "PA"},{"PI", "PI"},{"PU", "PU"},{"PE", "PE"},{"PO", "PO"},
        
        {"KYA","KYA"},{"KYU","KYU"},{"KYO","KYO"},
        {"SYA","SHA"},{"SYU","SHU"},{"SYO","SHO"},
        {"TYA","CHAA"},{"TYU","CHU"},{"TYO","CHO"},
        {"NYA","NYA"},{"NYU","NYU"},{"NYO","NYO"},
        {"HYA","HYA"},{"HYU","HYU"},{"HYO","HYO"},
        {"MYA","MYA"},{"MYU","MYU"},{"MYO","MYO"},
        {"RYA","RYA"},{"RYU","RYU"},{"RYO","RYU"},
        {"GYA","GYA"},{"GYU","GYU"},{"GYO","GYO"},
        {"ZYA","JA"},{"ZYU","JU"},{"ZYO","JO"},
        {"DYA","DYA"},{"DYU","DYU"},{"DYO","DYO"},
        {"BYA","BYA"},{"BYU","BYU"},{"BYOょ","BYO"},
        {"PYA","PYA"},{"PYU","PYU"},{"PYO","PYO"},
        {"VA","VA"},{"VI","VI"},{"VU","VU"},{"VE","VE"},{"VO","VO"},

    };
    //小文字
    public Dictionary<string, string> dicS = new Dictionary<string, string>() {

        {"あ", "a"},{"い", "i"},{"う", "u"},{"え", "e"},{"お", "o"},
        {"か", "ka"},{"き", "ki"},{"く", "ku"},{"け", "ke"},{"こ", "ko"},
        {"さ", "sa"},{"し", "si"},{"す", "su"},{"せ", "se"},{"そ", "so"},
        {"た", "ta"},{"ち", "ti"},{"つ", "tu"},{"て", "te"},{"と", "to"},
        {"な", "na"},{"に", "ni"},{"ぬ", "nu"},{"ね", "ne"},{"の", "no"},
        {"は", "ha"},{"ひ", "hi"},{"ふ", "hu"},{"へ", "he"},{"ほ", "ho"},
        {"ま", "ma"},{"み", "mi"},{"む", "mu"},{"め", "me"},{"も", "mo"},
        {"や", "ya"},{"ゆ", "yu"},{"よ", "yo"},
        {"ら", "ra"},{"り", "ri"},{"る", "ru"},{"れ", "re"},{"ろ", "ro"},
        {"わ", "wa"},{"を", "wo"},{"ん", "n"},
        {"が", "ga"},{"ぎ", "gi"},{"ぐ", "gu"},{"げ", "ge"},{"ご", "go"},
        {"ざ", "za"},{"じ", "zi"},{"ず", "zu"},{"ぜ", "ze"},{"ぞ", "zo"},
        {"だ", "da"},{"ぢ", "di"},{"づ", "du"},{"で", "de"},{"ど", "do"},
        {"ば", "ba"},{"び", "bi"},{"ぶ", "bu"},{"べ", "be"},{"ぼ", "bo"},
        {"ぱ", "pa"},{"ぴ", "pi"},{"ぷ", "pu"},{"ぺ", "pe"},{"ぽ", "po"},
        
        {"きゃ","kya"},{"きゅ","kyu"},{"きょ","kyo"},
        {"しゃ","sya"},{"しゅ","syu"},{"しょ","syo"},
        {"ちゃ","tya"},{"ちゅ","tyu"},{"ちょ","tyo"},
        {"にゃ","nya"},{"にゅ","nyu"},{"にょ","nyo"},
        {"ひゃ","hya"},{"ひゅ","hyu"},{"ひょ","hyo"},
        {"みゃ","mya"},{"みゅ","myu"},{"みょ","myo"},
        {"りゃ","rya"},{"りゅ","ryu"},{"りょ","ryo"},
        {"ぎゃ","gya"},{"ぎゅ","gyu"},{"ぎょ","gyo"},
        {"じゃ","zya"},{"じゅ","zyu"},{"じょ","zyo"},
        {"ぢゃ","dya"},{"ぢゅ","dyu"},{"ぢょ","dyo"},
        {"びゃ","bya"},{"びゅ","byu"},{"びょ","byo"},
       
        {"ぴゃ","pya"},{"ぴゅ","pyu"},{"ぴょ","pyo"},
        {"ヴぁ","va"},{"ヴぃ","vi"},{"ヴ","vu"},{"ヴぇ","ve"},{"ヴぉ","vo"},
       
        {"、","," },{"。","."},{"「","["},{"」","]"},
    };

    // デフォルトではない入力方法
    public Dictionary<string, List<string>> dicSEx = new Dictionary<string, List<string>>()
    {
        {"か", new List<string>{"ca"}},{"し", new List<string>{"ci","shi"}},{"く", new List<string>{"cu","qu"}},
        {"せ", new List<string>{"ce"}},{"こ", new List<string>{"co"}},{"ちゃ", new List<string>{"cha","cya"}},
        {"ち", new List<string>{"chi","cyi"}},{"ちゅ", new List<string>{"chu","cyu"}},
        {"ちぇ", new List<string>{"che","cye"}},{"ちょ", new List<string>{"cho","cyo"}},
        {"ふ", new List<string>{"fu"}},{"じゃ", new List<string>{"ja"}},{"じ", new List<string>{"ji"}},
        {"じゅ", new List<string>{"ju"}},{"じぇ", new List<string>{"je"}},{"じょ", new List<string>{"jo"}},
        {"ぁ", new List<string>{"la","xa"}},{"ぃ", new List<string>{"li","xi"}},{"ぅ", new List<string>{"lu","xu"}},
        {"ぇ", new List<string>{"le","xe"}},{"ぉ", new List<string>{"lo","xo"}},{"ゃ", new List<string>{"lya","xya"}},
        {"ゅ", new List<string>{"lyu","xyu"}},{"ょ", new List<string>{"lyo","xyo"}},
        {"っ", new List<string>{"ltu","xtu"}},{"ん", new List<string>{"nn"}},{"くぃ", new List<string>{"qyi"}},
        {"くぇ", new List<string>{"qye"}},{"しゃ", new List<string>{"sha"}},{"しゅ", new List<string>{"shu"}},
        {"しぇ", new List<string>{"she"}},{"しょ", new List<string>{"sho"}},{"つ", new List<string>{"tsu"}},
    };
}
