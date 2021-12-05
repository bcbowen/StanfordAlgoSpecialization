<Query Kind="Program">
  <Namespace>System.Numerics</Namespace>
</Query>

void Main()
{
	GetResult();

	//KaratsubaTester.RunTests();
}

private static void GetResult() 
{
	// 8539734222829339153914829626516087117397522044623384796795378019782377244052524520433524726598803573589515913102389712581823184
	string x = "3141592653589793238462643383279502884197169399375105820974944592";
	string y = "2718281828459045235360287471352662497757247093699959574966967627";
	string result = Karatsuba.Multiply(x, y);
	result.Dump();

}

static class KaratsubaTester 
{
	public static void RunTests() 
	{
		// 12319629722364981996463169030456 - 30860637998570837974268044477950
		
		TestMultiply();
		TestSubtract();
		TestAdd();
		TestSetSizes();
		TestSetSize();
		TestGetIdealSize();
	}

	public static void TestMultiply()
	{
		/**/
		TestMultiply("2", "4", "8");
		TestMultiply("20", "4", "80");
		TestMultiply("20", "40", "800");
		TestMultiply("134", "46", "6164");
		TestMultiply("5678", "1234", "7006652");
		TestMultiply("51", "46", "2346");
		TestMultiply("51", "42", "2142");
		TestMultiply("5127", "4265", "21866655");
		TestMultiply("12345678", "98765432", "1219326221002896");
		TestMultiply("9509090958164057267497022913300417684341772484307875629755329462831070821785127159670052473793464100979339895889746353138559887988294355346396259803963956030063880220813053988520717421913782288218444359253148866287554851687427214264758532702784990463166050", "8869905480101421754029658022141462145364998772684420847838410201867544038345041673755428184096957803661971239945690101337347013995167200299368957070993663445317591147229634611657120605175033869194756084492011109267497808621959974001499202660852430421078437", "84344738000602250980210229560377342055738602164488740852007006929634427109945893865395904502805297118504173683917365762878399944413683125751557728530859978327897128761550027477401223735321411766525621796968683955738120136166937150250111349031559080541174531850944486414473416871946505443695054145773932806672612096946257620988240827012344573143812652052381051062881749419289944158609033020095685713790224299058722086740763293438561848335444304474865756795507167041194528129488712693615316504298967291366405463850");
		
		TestMultiply("1", "2", "2");
		TestMultiply("6", "3", "18");
		TestMultiply("5", "6", "30");
		TestMultiply("8", "5", "40");
		TestMultiply("37", "32", "1184");
		TestMultiply("81", "89", "7209");
		TestMultiply("93", "76", "7068");
		TestMultiply("26", "71", "1846");
		TestMultiply("6937", "2423", "16808351");
		TestMultiply("5176", "4440", "22981440");
		TestMultiply("1385", "3751", "5195135");
		
		TestMultiply("127", "102", "12954");
		TestMultiply("5077", "8319", "42235563");
		
		
		// 13
		TestMultiply("80367228", "42582913", "3422270677975164");
		TestMultiply("14977670", "84664984", "1268084190907280");
		TestMultiply("80056470", "42533735", "3405100680015450");
		TestMultiply("90109928", "95084501", "8568057539025928");
		// 17
		TestMultiply("7878064237045606", "7349065192669285", "57896407670084570194037420411710");
		TestMultiply("3891696288463230", "1320643435224703", "5139543155247306769501603170690");
		TestMultiply("5055039710087460", "1404924844655896", "7101950879424010292543764664160");
		// 20
		TestMultiply("3708846298640594", "9081685595280321", "33682576005473018115324659950674");
		TestMultiply("66729899957367990709339108031112", "32340867005124202389268050080611", "2158102819786481370385134761940486771461182643704842409095969432");
		TestMultiply("71921099729304837578437758354458", "60864416737848794652827635439099", "4377435786168793770559850694671087852856933480748749102514153342");
		TestMultiply("59629178437692358531449397643379", "59629178437692358531449397643379", "3861491807063027921559993366672030909525675389594863963976783355");
		TestMultiply("13071612125297566953246476009714", "40742269461754956327492962194313", "532567143508416863901383839861994487901493714453361125743556482");
		// 25
		TestMultiply("3864243123455121010825830305805122665477124423845221767200334348", "3992049315503105496269956245117880000226792034950080536669272697", "15426249115926598218843017910317664060195261346965541656986566262588345553949120901395898889791941644835007391424324436387696556");
		TestMultiply("1547253203943221083821538910380041929832642546007865991833279992", "8491196094160787440367241887783002049017396089886383794259366567", "13138030362000443146985942399804125529128633094147605019661521170172937635978816395706171461253727813283706780071097515874827464");
		TestMultiply("2154470589002093683998754878220853750699219020159468165835454507", "9576151464435991850872678688874840784135181401750735422729351268", "20631536685956673350839602949241455982024149750065856886496370221891813571111439335609008255257626683465095897832169978036764876");
		TestMultiply("5211986647570828172548043773141856795012425179839383045438476733", "4606712684935304570646997185447986666259678494439329643332680432", "24010125003077966864485729529159557553067440688265053895552900715362892040776018932263952306533755390568862505742644387956388656");
		TestMultiply("78624642046507710938888309775009441060368035479983368921194424236729498279818304768532971365125084366714124105458846627616155684", "57579173234021428356201222308669471930637687238795395560984306955384912395268850754122737444531250516797690870019557285179017458", "4527141884858792569703012943690376879588033499441823212561236223396968611098565612973869852611827681292580539090160200871263453645942301937640563473106936130137478841069949884085208733024887924764700977723693142997968462661561226948749754204074730281931272");
		// 30
		TestMultiply("79289448939856945418479102243902489733345589179631910157140064357532907276632698678423739595802243127801639384397811791725173703", "89091164884767636135143691164688457225743558186918272352749046884013846041993990587523169651823197038091322158861169950329660096", "7063989369123159569792174904819432356604923365838848300299444332990849107568595633688320552714954177084218935086943879304156424537299771681456395668055517198141090958942775414007655363566454866277490501012696108351473323749604188690152863003877618547655488");
		TestMultiply("99615790486094417376090801668169164966124889237191368396003822190453363522518069831766512078559950872531851587461689758377693490", "85846470797846424040727649083188111668669506401258790859272612286556399320873443068516991447037214833598518508798979965495812015", "8551664048968892035764827310621985700104429181183039862339115181632334111008566449981139303677102152475615977150451813908707880217676806490783762930161151350713342129833506462421020497489849585329787664605997284508284088644995587365910055827035190329282350");
		TestMultiply("54912929456625402256174854408349005059747367418045281224869677103460587001371838509047325775339825409089700424382586661886548455", "46159761840763928340146255755371712831239294454920928659343103820160462080093196044837908676121279113445132081834876936121057294", "2534767745696498721291598226410808420875926624404989594109011804054230420328163716811636469827804550673802278410568911221048870743264706668346786117132264407551450821426489441139772838020127634525234251586349419324977858145598895358201970734412370962180770");
		TestMultiply("12345678998765432112345678987654321", "1212121212121234343434343434343434444444444", "14964459392443222363386654166355210140914770544956011896745221548821561042524");
		TestMultiply("11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111", "22222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222", "246913580246913580246913580246913580246913580246913580246913580246913580246913580246913580246913580246913580246913580246913580246913577777777777775308641975308641975308641975308641975308641975308641975308641975308641975308641975308641975308641975308641975308641975308641975308642");
		/**/
	}

	public static void TestMultiply(string x, string y, string expected)
	{
		string result = Karatsuba.Multiply(x, y);
		if (result == expected)
		{
			Console.WriteLine($"TestMultiply for {x}, {y} PASSED");
		}
		else
		{
			string diff = Karatsuba.Subtract(expected, result);
			Console.WriteLine($"TestMultiply for {x}, {y} FAILED; Expected {expected}, got {result} (diff: {diff})");
		}
	}

	public static void TestGetIdealSize() 
	{
		TestGetIdealSize("1", 1);		
		TestGetIdealSize("10", 2);
		TestGetIdealSize("103", 4);
		TestGetIdealSize("1234", 4);
		TestGetIdealSize("123456", 8);
		TestGetIdealSize("12345678", 8);
		TestGetIdealSize("1234567890", 16);
	}

	private static void TestGetIdealSize(string input, int expectedSize) 
	{
		int result = Karatsuba.GetIdealSize(input);
		if (result == expectedSize)
		{
			Console.WriteLine($"TestGetIdealSize for {input} PASSED");
		}
		else
		{
			Console.WriteLine($"TestGetIdealSize for {input} FAILED; Expected {expectedSize} got {result}");
		}
	}

	public static void TestSetSize()
	{
		TestSetSize("1", "1");
		TestSetSize("10", "10");
		TestSetSize("103", "0103");
		TestSetSize("1234", "1234");
		TestSetSize("123456", "00123456");
		TestSetSize("12345678", "12345678");
		TestSetSize("1234567890", "0000001234567890");
	}

	private static void TestSetSize(string input, string expected)
	{
		string result = Karatsuba.SetSize(input);
		if (result == expected)
		{
			Console.WriteLine($"TestSetSize for {input} PASSED");
		}
		else
		{
			Console.WriteLine($"TestSetSize for {input} FAILED; Expected {expected} got {result}");
		}
	}

	public static void TestSetSizes()
	{
		TestSetSizes("1", "2", "1", "2");
		TestSetSizes("13", "2", "13", "02");
		TestSetSizes("1", "10", "01", "10");
		TestSetSizes("103", "0103", "0103", "0103");
		TestSetSizes("1", "1234", "0001", "1234");
	}

	private static void TestSetSizes(string x, string y, string expectedX, string expectedY)
	{
		(string resultX, string resultY) = Karatsuba.SetSizes(x, y);
		if (resultX == expectedX && resultY == expectedY)
		{
			Console.WriteLine($"TestSetSizes for ({x}, {y}) PASSED");
		}
		else
		{
			Console.WriteLine($"TestSetSizes for ({x}, {y}) FAILED; Expected ({expectedX}, {expectedY}) got ({resultX}, {resultY})");
		}
	}

	public static void TestAdd() 
	{
		TestAdd("1", "2", "3");
		TestAdd("100", "234", "334");
		TestAdd("56", "78", "134");
		TestAdd("1000", "80", "1080");
		TestAdd("9999", "9999", "19998");
		
	}
	
	public static void TestAdd(string x, string y, string expected)
	{
		string result = Karatsuba.Add(x, y);
		if (result == expected)
		{
			Console.WriteLine($"TestAdd for {x}, {y} PASSED");
		}
		else
		{
			Console.WriteLine($"TestAdd for {x}, {y} FAILED; Expected {expected}, got {result}");
		}
	}

	public static void TestSubtract()
	{
		TestSubtract("3", "2", "1");
		TestSubtract("334", "234", "100");
		TestSubtract("134", "78", "56");
		TestSubtract("1000", "999", "1");
		TestSubtract("193616992", "32190022", "161426970");
		/*
Subtracting 150339528 from 161426970
Subtracting 123196274286865 from 32191130894539528
Subtracting 30774256169030456 from 32067934620252663
Subtracting  from 
Subtracting 17326637100882997738932031487211 from 81458991723794144022195125562506
Subtracting 855073323256315604636843984142224592335936887645128270808885170 from 3086063799857084438750350676906490159268041635947738932031487211
Subtracting 904303566101508307579874104780474589513417013291713582823184 from 2231990476600768834123516692764275566932104748302610661222602041
8550733232563156046368439841422469031976682343183864301770717649510138562699240441202343862090480474589513417013291713582823184
		*/
	}

	public static void TestSubtract(string x, string y, string expected)
	{
		string result = Karatsuba.Subtract(x, y);
		if (result == expected)
		{
			Console.WriteLine($"TestSubtract for {x}, {y} PASSED");
		}
		else
		{
			Console.WriteLine($"TestSubtract for {x}, {y} FAILED; Expected {expected}, got {result}");
		}
	}

	

}

// You can define other methods, fields, classes and namespaces here

static class Karatsuba 
{

	

	public static string Multiply(string x, string y)
	{
		if (x.Length < 4 && y.Length < 4)
		{
			try
			{
				if (int.Parse(x) == 0 || int.Parse(y) == 0) return "0";
				if (x.Length == 1 || y.Length == 1) return (int.Parse(x) * int.Parse(y)).ToString();
			}
			catch(FormatException ex)
			{
				Console.WriteLine($"Format Exception for x: {x}; y: {y}");
			}
			
		}
		
		
		(x, y) = SetSizes(x, y); 
		int xLen = x.Length;
		int yLen = y.Length;
		int n = Math.Min(xLen, yLen);
		x = x.PadLeft(n, '0');
		y = y.PadLeft(n, '0');
		int halfN = (int)(Math.Ceiling((double)n/2));
		string a, b, c, d;
		(a, b) = SplitValue(x, halfN);
		//string a = x.Substring(0, halfN);
		//string b = xLen > halfN ? x.Substring(halfN, xLen - halfN) : "0";

		//string c = y.Substring(0, halfN);
		//string d = yLen > halfN ? y.Substring(halfN, yLen - halfN) : "0";
		(c, d) = SplitValue(y, halfN);

		string step1 = Multiply(a, c);
		string step2 = Multiply(b, d);
		string step3a = Multiply(Add(a, b), Add(c, d));
		string step3b = Subtract(step3a, step1);
		string step3 = Subtract(step3b, step2);
		// subtract step 2 from step 3
		
		string resulta = step1.PadRight(step1.Length + n, '0'); 
		string resultb = step3.PadRight(step3.Length + halfN, '0'); 
		string result = Add(Add(resulta, resultb), step2);

		//value = Subtract(value, step1.PadRight(step1.Length + x.Length / 2, '0'));

		return result;
	}

	private static (string, string) SplitValue(string value, int n)
	{
		string y = value.Substring(value.Length - n);
		string x = value.Substring(0, value.Length - n);
		
		string pattern = "^0+$"; 
		x = Regex.IsMatch(x, pattern) ? "0" : x.TrimStart('0');

		return (x, y);
	}

	/*
	// subtract y from x
	internal static string Subtract(string x, string y) 
	{
		//Console.WriteLine($"Subtracting {y} from {x}"); 
		if (x == y) return "0"; 
		bool borrow = false;
		StringBuilder result = new StringBuilder();

		if (y.Length < x.Length) y = y.PadLeft(x.Length, '0');

		int top;
		int bottom;
		int difference;
		for (int i = x.Length -1; i >= 0; i--)
		{
			top = int.Parse(x.Substring(i, 1));
			bottom = int.Parse(y.Substring(i, 1));
			if (borrow) top = top > 0 ? top - 1 : 9;
			if (top >= bottom)
			{
				borrow = false;
			}
			else
			{
				borrow = true; 
				top += 10;
			}
			difference = top - bottom;
			result.Insert(0, difference);
		
		}

		return result.ToString().TrimStart('0');

	}
	*/

	// subtract y from x
	internal static string Subtract(string x, string y) 
	{
		BigInteger i = BigInteger.Parse(x); 
		BigInteger j = BigInteger.Parse(y); 
		return BigInteger.Subtract(i, j).ToString();
	}

	internal static string Add(string x, string y)
	{
		BigInteger i = BigInteger.Parse(x);
		BigInteger j = BigInteger.Parse(y);
		return BigInteger.Add(i, j).ToString();
	}

	/*
	internal static string Add (string x, string y)
	{
		int carry = 0; 
		int top;
		int bottom;
		int sum;
		int n = Math.Max(x.Length, y.Length);
		x = x.PadLeft(n, '0');
		y = y.PadLeft(n, '0');
		if (x.Length == 1 || y.Length == 1) 
		{
			return (int.Parse(x) + int.Parse(y)).ToString();
		}
		StringBuilder result = new StringBuilder(); 
		for (int i = x.Length - 1; i >= 0; i--) 
		{
			top = int.Parse(x.Substring(i, 1)); 
			bottom = int.Parse(y.Substring(i, 1));
			sum = top + bottom + carry;
			if (sum > 9)
			{
				carry = 1;
				result.Insert(0, sum - 10);
			}
			else
			{
				carry = 0; 
				result.Insert(0, sum);
			}
		}
		if (carry == 1)
		{
			result.Insert(0, carry);
		}
		
		return result.ToString();
	}
	*/

	internal static (string, string) SetSizes(string x, string y) 
	{
		x = SetSize(x); 
		y = SetSize(y);

		if (x.Length > y.Length)
		{ 
			y = y.PadLeft(x.Length, '0');
		}
		else if (y.Length > x.Length)
		{
			x = x.PadLeft(y.Length, '0');
		}
		
		return (x, y);
	}

	internal static string SetSize(string value) 
	{
		int len = GetIdealSize(value);
		if (value.Length == len) 
		{
			return value;
		}
		
		return value.PadLeft(len, '0');
	}

	internal static int GetIdealSize(string value)
	{
		if (string.IsNullOrEmpty(value)) return 0;

		// if length is 1 or 2, leave it as it is
		if (value.Length < 3) return value.Length;

		// if the length is already a power of 2 it is good
		if (Math.Log2(value.Length) % 1 == 0) return value.Length;
		
		int testLen = 2;
		while(testLen < value.Length) 
		{
			testLen *= 2;
		}
		
		return testLen;
	}
	
}