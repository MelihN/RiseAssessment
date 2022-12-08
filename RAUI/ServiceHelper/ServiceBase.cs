using System.Net.Http.Headers;

namespace RAUI.ServiceHelper
{
    public class ServiceBase
    {        
        public static HttpClient ServiceClient(string baseUrl)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = TimeSpan.FromMinutes(5);
            byte[] cred = System.Text.UTF8Encoding.UTF8.GetBytes("RaTestUser:p@ssw0rdRa");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(cred));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        public string CheckServiceStatusError(System.Net.HttpStatusCode code)
        {
            var result = string.Empty;
            switch (code)
            {
                case System.Net.HttpStatusCode.NotFound:
                    result = "404 - Servis bulunamadı.";
                    break;
                case System.Net.HttpStatusCode.BadRequest:
                    result = "400 - Servis istek hatası.";
                    break;
                case System.Net.HttpStatusCode.InternalServerError:
                    result = "500 - Servis istek hatası.";
                    break;
                case System.Net.HttpStatusCode.Created:
                    result = "201 - Servis sorgusu başarılı. Ancak sonuç için yeni bir kaynak oluşturuldu.";
                    break;
                case System.Net.HttpStatusCode.NoContent:
                    result = "204 - Servis sorgusu başarılı. Ancak sunucu cevap vermiyor.";
                    break;
                case System.Net.HttpStatusCode.Conflict:
                    result = "409 - Servis sorgulamada çakışma oluştuğu için istek tamamlanamadı.";
                    break;
                case System.Net.HttpStatusCode.PreconditionFailed:
                    result = "412 - Servisin kabul etmediği başlık bilgileri ile sorgulama yapılmış.";
                    break;
                case System.Net.HttpStatusCode.Ambiguous:
                    result = "300 - Servis belirsiz istek hatası.";
                    break;
                case System.Net.HttpStatusCode.BadGateway:
                    result = "502 - Sunucu, isteği işlemek için gereken yanıtı almak üzere bir ağ geçidi olarak çalışırken geçersiz bir yanıt aldı.";
                    break;
                case System.Net.HttpStatusCode.EarlyHints:
                    result = "103 - Öncelikle Link başlığı ile kullanılmak üzere tasarlanmıştır. Sunucu son yanıtı hazırlarken kullanıcı aracısının kaynakları önceden yüklemeye başlamasını önerir.";
                    break;
                case System.Net.HttpStatusCode.ExpectationFailed:
                    result = "417 - Beklenti isteği başlık alanı tarafından belirtilen beklenti, sunucu tarafından karşılanamaz.";
                    break;  
                case System.Net.HttpStatusCode.FailedDependency:
                    result = "424 - İstek, önceki bir isteğin başarısız olması nedeniyle başarısız oldu.";
                    break;
                case System.Net.HttpStatusCode.Forbidden:
                    result = "403 - Gelecekte kullanılmak üzere rezerve edilmiştir. Dijital ödeme sistemlerinde kullanılması hedeflenmektedir.";
                    break;
                case System.Net.HttpStatusCode.GatewayTimeout:
                    result = "504 - Sunucu bir ağ geçidi görevi görüyor ve bir istek için zamanında yanıt alamıyor.";
                    break;
                case System.Net.HttpStatusCode.HttpVersionNotSupported:
                    result = "505 - İstekte kullanılan HTTP sürümü sunucu tarafından desteklenmiyor.";
                    break;
                case System.Net.HttpStatusCode.InsufficientStorage:
                    result = "507 - Sunucu, isteği başarıyla tamamlamak için gereken gösterimi depolayamadığından, yöntem kaynakta gerçekleştirilemedi.";
                    break;
                case System.Net.HttpStatusCode.LengthRequired:
                    result = "411 - Sunucu, tanımlanmış bir İçerik Uzunluğu olmadan isteği kabul etmeyi reddediyor. İstemci, geçerli bir Content-Length başlık alanı eklerse isteği tekrarlayabilir.";
                    break;
                case System.Net.HttpStatusCode.Locked:
                    result = "423 - Erişilmekte olan kaynak kilitli.";
                    break;
                case System.Net.HttpStatusCode.LoopDetected:
                    result = "508 - Sunucu, isteği işlerken sonsuz bir döngü algıladı.";
                    break;
                case System.Net.HttpStatusCode.MethodNotAllowed:
                    result = "405 - İstek HTTP yöntemi sunucu tarafından biliniyor ancak devre dışı bırakıldı ve bu kaynak için kullanılamaz.";
                    break;
                case System.Net.HttpStatusCode.MisdirectedRequest:
                    result = "421 - İstek yanıt üretemeyen bir sunucuya yönlendirildi.";
                    break;
                case System.Net.HttpStatusCode.NetworkAuthenticationRequired:
                    result = "511 - İstemcinin ağ erişimi kazanmak için kimlik doğrulaması yapması gerektiğini belirtir.";
                    break;
                case System.Net.HttpStatusCode.NonAuthoritativeInformation:
                    result = "203 - Varlık başlığında döndürülen meta bilgilerin, kaynak sunucudan elde edilen kesin küme olmadığını, yerel veya üçüncü taraf bir kopyadan toplandığını belirtir. Sunulan set, orijinal versiyonun bir alt seti veya üst seti olabilir.";
                    break;
                case System.Net.HttpStatusCode.NotAcceptable:
                    result = "406 - Sunucu, istekte gönderilen Kabul Et başlığında kullanıcı aracısı tarafından verilen kriterlere uyan herhangi bir içerik bulamaz.";
                    break;
                case System.Net.HttpStatusCode.NotImplemented:
                    result = "501 - HTTP yöntemi sunucu tarafından desteklenmiyor ve işlenemiyor.";
                    break;
                case System.Net.HttpStatusCode.ProxyAuthenticationRequired:
                    result = "407 - İstemcinin önce proxy ile kimliğini doğrulaması gerektiğini belirtir.";
                    break;
                case System.Net.HttpStatusCode.RequestEntityTooLarge:
                    result = "413 - İstek varlığı, sunucu tarafından tanımlanan sınırlardan daha büyük.";
                    break;
                case System.Net.HttpStatusCode.RequestTimeout:
                    result = "408 - Sunucunun, sunucuya ayrılan zaman aşımı süresi içinde istemciden tam bir istek almadığını gösterir.";
                    break;
                case System.Net.HttpStatusCode.UnsupportedMediaType:
                    result = "415 - İsteğin İçerik türündeki ortam türü, sunucu tarafından desteklenmiyor.";
                    break;
                case System.Net.HttpStatusCode.Unauthorized:
                    result = "401 - İsteğin kullanıcı kimlik doğrulama bilgisi gerektirdiğini belirtir. İstemci, isteği uygun bir Yetkilendirme başlık alanıyla tekrarlayabilir.";
                    break;
                case System.Net.HttpStatusCode.TooManyRequests:
                    result = "429 - Kullanıcı, belirli bir süre içinde çok fazla istek gönderdi (\"oran sınırlaması\").";
                    break;
                case System.Net.HttpStatusCode.ServiceUnavailable:
                    result = "503 - Sunucu isteği işlemeye hazır değil.";
                    break;
                case System.Net.HttpStatusCode.RequestUriTooLong:
                    result = "414 - İstemci tarafından istenen URI, sunucunun yorumlayabileceğinden daha uzun.";
                    break;
            }
           return result;
        }
    }
}
