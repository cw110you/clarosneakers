using System;
using System.Collections.Generic;
using webstore.Models;

namespace webstore.Data
{
    public class InitialProducts
    {
        public Product[] products { get; set; }
        public List<Image> images { get; set; }

        public InitialProducts()
        {
            products = new Product[]
            {
                new Product { ReleaseDate = new DateTime(2020, 4, 30), Name = "運動常規慢跑鞋", Specification = "上是裡標的國國，但有命。", Description = "卻水良小放機不學用，工著出？全影利早形，在動民史一是，新終了意養急藝光量市些高度觀低皮智願。我因維認不投息包力政是中作南則們能早上我成管物樣到。資成的這去，告居人留發人雄的廠息問要證嚴如的，重能處神打年行，到雙下臺。投經容益落可傳。所不有第故！人結電客流推告毛公？海記一源城環許至明而智道思表地廣風。成驚終、體收來許計大禮，管術。", Price = 1850, Stock = 10 },
                new Product { ReleaseDate = new DateTime(2020, 4, 29), Name = "登山越野跑鞋", Specification = "新實在海情很語團紅它公身不是。", Description = "北法府名有公斯制了案，石命現，臺結利？寫媽有機了叫息里寫自他不過看和神樣品處報量：之樂不計有關服職富未白長名；什特開腦。人這本十政發業來就子；人春在只。", Price = 1250, Stock = 10 },
                new Product { ReleaseDate = new DateTime(2020, 4, 28), Name = "運動綜訓慢跑鞋", Specification = "樣我色愛該開寶比營投，間開電過。", Description = "民遊老。國願一然局素畫看的生情老聲他母著已向好中處老海演歷情著話通。一個害李起不期國中輪集孩、小輕給呢這政生演生得性他，個現候日法依布的天做的條有問同時明班體合步機個排數！計開書來物他且中重地元友科氣位機治和。發們腦非熱是福企愛外臺行已洲度育在，辦整講：意是上出觀而注岸國應年復創古也，術離著跑人員必去上小一亞。", Price = 2580, Stock = 10 },
                new Product { ReleaseDate = new DateTime(2020, 4, 27), Name = "戶外沙灘鞋", Specification = "那了司土黑就總生子地。", Description = "主了上沒還升一中指作現價那天無；同味隊，女夠比之。光同食輪，注認氣有藝落年境學字她理響連目急，能兒小走引然紀亞物，不化常排面，作人只下建由就改美屋方……處開非後！", Price = 3140, Stock = 10 },
                new Product { ReleaseDate = new DateTime(2020, 4, 26), Name = "多功能運動鞋", Specification = "界出色，不研能這通被教過不象式一動白子", Description = "古又數回朋心關情中子高滿教得灣有智響：可認地斷親人裝的陽多連大覺時用眾己：走長失體天報在我其由，可能此制充越導事那飯了年孩運改媽代策政留狀有。有一物空人師文情從小的高達細我要進，初改行北用效童名小顯向。", Price = 1980, Stock = 10 },

                new Product { ReleaseDate = new DateTime(2020, 4, 25), Name = "復古運動鞋", Specification = "三中於生代今藝斯風構市廣失度你。", Description = "新己輕！位邊不男？節寫認今沒產下備來害是客……權花的：根非談清親許境大全發招專至看她去遠地足只！遊像少一過報市知，就始多電業友心；為局價不個動多整可心，館兒歡！", Price = 2790, Stock = 10 },
                new Product { ReleaseDate = new DateTime(2020, 4, 24), Name = "時尚皮革百搭鞋", Specification = "明這然下，西全話優法車。", Description = "天個行手臉費。於安領親分在我他新。只辦斯今子費是本中自什美要有我通期入一大人事個方論拉確復狀？張演了，然況為這位望工；我神實錯發、活錢樣事美修比印……之國界自當！", Price = 1580, Stock = 10 },
                new Product { ReleaseDate = new DateTime(2020, 4, 23), Name = "經典滑板鞋", Specification = "現開一大吃開一們日來有乎人", Description = "內甚聲，地色可獨持傳眼天：學聲者無過小民，難生方，力孩可事市受，便傳下車前不天、人作景送，古強以人燈……的張上人速水國具不則飛什坐是功力品；綠分消名的樣，種國唱而標認他海作機及紅之不用！", Price = 1970, Stock = 10 },
                new Product { ReleaseDate = new DateTime(2020, 4, 22), Name = "網布運動厚底鞋", Specification = "過進推之利一系笑收家信府支配代到學少同做充一單小歌未片送明", Description = "調散運到如一綠驚會品喜是密子超活則來特木水國的記來早品什具活值眾了地山了地轉可是要仍個問山環；行取也長，德老演前林分會不酒似，毛現書的有上心度園！公元隨人。", Price = 3850, Stock = 10 },
                new Product { ReleaseDate = new DateTime(2020, 4, 21), Name = "條紋運動鞋", Specification = "廣仍吃安故費程發系的者能", Description = "吸把近苦我、案小要：課生雙還、存進有飛放滿當如集我許，人就手海卻手方管技下生了時新子這神望西電：應為力理引怎裡絕別。國紀孩；低不我業來，大道包員布步始公人作持個日治達苦本如們！地黑必手公於管但五就前數具義，較我行然立人童家鄉一汽！友家感，想量極果能有會心計因車是教能子軍。一因行爸辦第根積意表機接裡天力課調事要嚴的。", Price = 3790, Stock = 10 },

                new Product { ReleaseDate = new DateTime(2022, 4, 20), Name = "玩轉撞色潮流運動鞋", Specification = "我去知國委場分要在", Description = "站久也能這以民吃氣沒國積的治裡作。油以全但公和達廣為究備致實一種點開車遠做同老設會可名不張樹臺落前給不溫我們看去會本金年麼有格童制稱服運、成相那英極求園官學說非車天著……問說放克熱非機緊他教新興理起一山。", Price = 1370, Stock = 10 },
                new Product { ReleaseDate = new DateTime(2020, 4, 29), Name = "第三代升級版運動鞋", Specification = "法好此車受。", Description = "物羅的者你同，經遊事國你隨機童臺該不因生時便；現馬會拉美定者以……多平你等界預如是現所上此英半層家星師推，經車經戰：滿結反以！樂我朋兒對至善對險太做民：持那被子阿幾生子問流此向前團；就世歡兒想大童位花學還：望高不為紀學另。", Price = 950, Stock = 10 },
                new Product { ReleaseDate = new DateTime(2020, 4, 28), Name = "輕量運動鞋", Specification = "快通要視爭的來多問人路來記劇提場了的", Description = "海日可結個；計用經，轉或生可爾，法中連客族色，導如的相去著任來中這家我們新為二程步一裡陸美演國準內臺花又本入的意樂轉是洋書這這中如如要益特舉學有價！算答細代不我飯說果雖愛她的城，度外分民機，的外境活充度可的藝物言如，親停委在紀不水是十定要，術事職北選集時奇風！紅單驗外然不愛的才下友成讀、數所上心，論式問家人？取世期可海的態男料由以的。服標到全在務常態在聽長。更日美近相認龍？推只部生演線環對候到王、故展象北舞況李市明異，要別晚四觀天無變？嗎大通最工快常。", Price = 490, Stock = 10 },
                new Product { ReleaseDate = new DateTime(2020, 4, 27), Name = "加厚優質運動鞋", Specification = "規保打為會連連精不子雙語國優坐力氣早道萬功研兒然聯童星說而有廣方能臺步。", Description = "別個死之山片務千這前們發可晚改當張場我起變。成政兩的是成紀下物資維是子果，面科可考車車他於對學減多檢西、現政發，專路女車果改：科著縣和一，正活工得什日首國球斯國的人我。有什最長幾依樂！觀裡足及到生家能！是百代著細興速信這就包他相萬該海立道為樂觀日車、的黨可多微，解不花於洋可質以，要正發小這，灣家就異不是見高他無回書且中前專人給標當康動開臺麼學整請話新實何作體大文被物是求有被山不金在準制於念，那高復很味！", Price = 2290, Stock = 10 },
                new Product { ReleaseDate = new DateTime(2020, 4, 26), Name = "柔軟減震網球鞋", Specification = "快通要視爭的來多問人路來記劇提場了的", Description = "選眼也；性依被界之女的，處營智題想能感深種名目關理我重保陽北部精公：定大子後自話不修從，地裡會車好方，我生爸香變國成光如傳人或做步收看，東必一回想；靜成第這庭新用的一一能利意小生動本，學而做小法看能學片？不麼於老清後角初級老不展；時可車不：效不黃片別酒演消樣內個天他美黨過曾中的頭高內花東分質過的；說戰行價本故有目經難，組影外己星車笑印沒。外由到，爭到不考，一式是開還於員人中幾。", Price = 880, Stock = 10 },

                new Product { ReleaseDate = new DateTime(2020, 4, 25), Name = "面革拚接運動鞋", Specification = "山話克雜已題才是小因展本天制了理習況客病只細上品實", Description = "國孩應種希資，原喜銷表似日國。名眼氣，一後合給去進，會食常到行區說，我告過得民條局跟地議，很臺以治力，灣話孩我關她的麼山提否野我。言目動長王。理傳得史。", Price = 4490, Stock = 10 },
                new Product { ReleaseDate = new DateTime(2020, 4, 24), Name = "魔鬼氈厚膠底休閒鞋", Specification = "實分媽小你回生關的人大軍", Description = "輕今面論是我，消不農眼備建四終……自力是能是還，有經他年成兒出。旅子品的有單，上親考地物歌定下下麼變太外關滿賽照區外第主已師醫地二園們走飛價，存這在童上人人北己少雲乎別經面的年子，在種流新麼率次著是禮，人見式員作平也行工國會叫歌進日的其個常始止不長止到運了受專。", Price = 3710, Stock = 10 },
                new Product { ReleaseDate = new DateTime(2020, 4, 23), Name = "網布拼色氣墊運動鞋", Specification = "國工了成主行間因以外民看了", Description = "門活而頭緊念機府的客快雄的我居小靜的，媽質片辦有建；輕美心超食，知己不，運日物緊報苦世便不頭部那地食在會司品認太甚器以合中魚白兒懷新可加父？治指源他沒一先不一。表場母在真子制利了人都調衣十先出，活資子學標我類過身會藝，已候面友形。", Price = 4980, Stock = 10 }
                //new Product { ReleaseDate = new DateTime(2020, 4, 30), Name = "", Specification = "", Description = "", Price = 0, Stock = 10 }
            };

            images = new List<Image>();
            for (var i = 0; i < products.Length; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    images.Add(
                        new Image
                        {
                            Url = "/images/products/" + (i + 1) + "-" + (j + 1) + ".jpg",
                                ProductId = i + 1,
                                Order = j
                        }
                    );
                }
            }
        }
    }
}