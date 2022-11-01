﻿using System;
using NUnit.Framework;
using Algorithms.Math;

namespace Algorithms.Tests.Math
{
    [TestFixture]
    public class KaratsubaTests
    {

		/*

		
         */

		

		[TestCase("12345678998765432112345678987654321", "1212121212121234343434343434343434444444444", "14964459392443222363386654166355210140914770544956011896745221548821561042524")]
		[TestCase("3864243123455121010825830305805122665477124423845221767200334348", "3992049315503105496269956245117880000226792034950080536669272697", "15426249115926598218843017910317664060195261346965541656986566262588345553949120901395898889791941644835007391424324436387696556")]
		[TestCase("1547253203943221083821538910380041929832642546007865991833279992", "8491196094160787440367241887783002049017396089886383794259366567", "13138030362000443146985942399804125529128633094147605019661521170172937635978816395706171461253727813283706780071097515874827464")]
		[TestCase("2154470589002093683998754878220853750699219020159468165835454507", "9576151464435991850872678688874840784135181401750735422729351268", "20631536685956673350839602949241455982024149750065856886496370221891813571111439335609008255257626683465095897832169978036764876")]
		[TestCase("5211986647570828172548043773141856795012425179839383045438476733", "4606712684935304570646997185447986666259678494439329643332680432", "24010125003077966864485729529159557553067440688265053895552900715362892040776018932263952306533755390568862505742644387956388656")]
		[TestCase("79289448939856945418479102243902489733345589179631910157140064357532907276632698678423739595802243127801639384397811791725173703", "89091164884767636135143691164688457225743558186918272352749046884013846041993990587523169651823197038091322158861169950329660096", "7063989369123159569792174904819432356604923365838848300299444332990849107568595633688320552714954177084218935086943879304156424537299771681456395668055517198141090958942775414007655363566454866277490501012696108351473323749604188690152863003877618547655488")]
		[TestCase("99615790486094417376090801668169164966124889237191368396003822190453363522518069831766512078559950872531851587461689758377693490", "85846470797846424040727649083188111668669506401258790859272612286556399320873443068516991447037214833598518508798979965495812015", "8551664048968892035764827310621985700104429181183039862339115181632334111008566449981139303677102152475615977150451813908707880217676806490783762930161151350713342129833506462421020497489849585329787664605997284508284088644995587365910055827035190329282350")]
		[TestCase("54912929456625402256174854408349005059747367418045281224869677103460587001371838509047325775339825409089700424382586661886548455", "46159761840763928340146255755371712831239294454920928659343103820160462080093196044837908676121279113445132081834876936121057294", "2534767745696498721291598226410808420875926624404989594109011804054230420328163716811636469827804550673802278410568911221048870743264706668346786117132264407551450821426489441139772838020127634525234251586349419324977858145598895358201970734412370962180770")]
		[TestCase("78624642046507710938888309775009441060368035479983368921194424236729498279818304768532971365125084366714124105458846627616155684", "57579173234021428356201222308669471930637687238795395560984306955384912395268850754122737444531250516797690870019557285179017458", "4527141884858792569703012943690376879588033499441823212561236223396968611098565612973869852611827681292580539090160200871263453645942301937640563473106936130137478841069949884085208733024887924764700977723693142997968462661561226948749754204074730281931272")]
		[TestCase("11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111", "22222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222", "246913580246913580246913580246913580246913580246913580246913580246913580246913580246913580246913580246913580246913580246913580246913577777777777775308641975308641975308641975308641975308641975308641975308641975308641975308641975308641975308641975308641975308641975308641975308642")]
		[TestCase("9509090958164057267497022913300417684341772484307875629755329462831070821785127159670052473793464100979339895889746353138559887988294355346396259803963956030063880220813053988520717421913782288218444359253148866287554851687427214264758532702784990463166050", "8869905480101421754029658022141462145364998772684420847838410201867544038345041673755428184096957803661971239945690101337347013995167200299368957070993663445317591147229634611657120605175033869194756084492011109267497808621959974001499202660852430421078437", "84344738000602250980210229560377342055738602164488740852007006929634427109945893865395904502805297118504173683917365762878399944413683125751557728530859978327897128761550027477401223735321411766525621796968683955738120136166937150250111349031559080541174531850944486414473416871946505443695054145773932806672612096946257620988240827012344573143812652052381051062881749419289944158609033020095685713790224299058722086740763293438561848335444304474865756795507167041194528129488712693615316504298967291366405463850")]
		public void MultiplyTestsBig(string a, string b, string expectedResult) 
        {
			DateTime testStart = DateTime.Now;
			string result = Karatsuba.Multiply(a, b);
			Console.WriteLine($"Test completed in {DateTime.Now.Subtract(testStart).TotalMilliseconds} milliseconds.");
			Assert.AreEqual(expectedResult, result);

		}

		[TestCase("12345678", "98765432", "1219326221002896")]
		[TestCase("80367228", "42582913", "3422270677975164")]
		[TestCase("14977670", "84664984", "1268084190907280")]
		[TestCase("80056470", "42533735", "3405100680015450")]
		[TestCase("90109928", "95084501", "8568057539025928")]
		[TestCase("7878064237045606", "7349065192669285", "57896407670084570194037420411710")]
		[TestCase("3891696288463230", "1320643435224703", "5139543155247306769501603170690")]
		[TestCase("5055039710087460", "1404924844655896", "7101950879424010292543764664160")]
		[TestCase("3708846298640594", "9081685595280321", "33682576005473018115324659950674")]
		[TestCase("66729899957367990709339108031112", "32340867005124202389268050080611", "2158102819786481370385134761940486771461182643704842409095969432")]
		[TestCase("71921099729304837578437758354458", "60864416737848794652827635439099", "4377435786168793770559850694671087852856933480748749102514153342")]
		[TestCase("13071612125297566953246476009714", "40742269461754956327492962194313", "532567143508416863901383839861994487901493714453361125743556482")]
		public void MultiplyTestsMedium(string a, string b, string expectedResult)
		{
			DateTime testStart = DateTime.Now;
			string result = Karatsuba.Multiply(a, b);
			Console.WriteLine($"Test completed in {DateTime.Now.Subtract(testStart).TotalMilliseconds} milliseconds.");
			Assert.AreEqual(expectedResult, result);

		}

		[TestCase("2", "4", "8")]
		[TestCase("1", "2", "2")]
		[TestCase("6", "3", "18")]
		[TestCase("5", "6", "30")]
		[TestCase("8", "5", "40")]
		[TestCase("20", "4", "80")]
		[TestCase("37", "32", "1184")]
		[TestCase("81", "89", "7209")]
		[TestCase("20", "40", "800")]
		[TestCase("134", "46", "6164")]
		[TestCase("5678", "1234", "7006652")]
		[TestCase("51", "46", "2346")]
		[TestCase("51", "42", "2142")]
		[TestCase("93", "76", "7068")]
		[TestCase("26", "71", "1846")]
		[TestCase("6937", "2423", "16808351")]
		[TestCase("5176", "4440", "22981440")]
		[TestCase("1385", "3751", "5195135")]
		[TestCase("127", "102", "12954")]
		[TestCase("5077", "8319", "42235563")]
		[TestCase("5127", "4265", "21866655")]
		public void MultiplyTestsSmall(string a, string b, string expectedResult)
		{
			DateTime testStart = DateTime.Now;
			string result = Karatsuba.Multiply(a, b);

			Console.WriteLine($"Test completed in {DateTime.Now.Subtract(testStart).TotalMilliseconds} milliseconds.");
			Assert.AreEqual(expectedResult, result);

		}

		[TestCase("1", 1)]
		[TestCase("10", 2)]
		[TestCase("103", 4)]
		[TestCase("1234", 4)]
		[TestCase("123456", 8)]
		[TestCase("12345678", 8)]
		[TestCase("1234567890", 16)]
		public void GetIdealSizeTests(string value, int expectedSize) 
		{
			int result = Karatsuba.GetIdealSize(value);
			Assert.AreEqual(expectedSize, result);
		}

		[TestCase("1", "1")]
		[TestCase("10", "10")]
		[TestCase("103", "0103")]
		[TestCase("1234", "1234")]
		[TestCase("123456", "00123456")]
		[TestCase("12345678", "12345678")]
		[TestCase("1234567890", "0000001234567890")]
		public void SetSizeTests(string value, string expected)
		{
			string result = Karatsuba.SetSize(value);
			Assert.AreEqual(expected, result);
		}

		[TestCase("1", "2", "1", "2")]
		[TestCase("13", "2", "13", "02")]
		[TestCase("1", "10", "01", "10")]
		[TestCase("103", "0103", "0103", "0103")]
		[TestCase("1", "1234", "0001", "1234")]
		public void TestSetSizes(string x, string y, string expectedX, string expectedY)
		{
			(string resultX, string resultY) = Karatsuba.SetSizes(x, y);
			Assert.AreEqual(expectedX, resultX);
			Assert.AreEqual(expectedY, resultY);

		}

        [TestCase("11", "123", "134")]
        [TestCase("456", "77", "533")]
        [TestCase("0", "0", "0")]
        [TestCase("0", "9", "9")]
        [TestCase("9", "0", "9")]
        [TestCase("4600", "1564", "6164")]
        [TestCase("0000", "4600", "4600")]
        [TestCase("00000", "4600", "4600")]
        [TestCase("000", "4600", "4600")]
        //[InlineData("", "", "")]
        public void TestAdd(string x, string y, string expected)
        {
            string result = Karatsuba.Add(x, y);
            Assert.AreEqual(expected, result);
        }
    }
}
