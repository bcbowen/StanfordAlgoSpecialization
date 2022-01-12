using System.Linq;

namespace Algorithms
{
	static class InversionCounter
	{

		public static long CountInversionsBrute(int[] values)
		{
			long count = 0;
			for (int i = 0; i < values.Length - 1; i++)
			{
				for (int j = i + 1; j < values.Length; j++)
				{
					if (values[i] > values[j]) count++;
				}
			}

			return count;
		}

		public static long CountInversions(int[] values)
		{
			(int[] sortedValues, long inversionCount) = SortAndCount(values);
			return inversionCount;
		}

		private static (int[] sortedValues, long inversionCount) SortAndCount(int[] values)
		{
			long inversionCount = 0;
			if (values.Length < 2)
			{
				return (values, inversionCount);
			}
			else
			{
				int n = values.Length / 2;
				long inversions = 0;
				int[] leftValues, rightValues, sortedValues;
				(leftValues, inversions) = SortAndCount(values.Take(n).ToArray());
				inversionCount += inversions;
				(rightValues, inversions) = SortAndCount(values.Skip(n).Take(values.Length - n).ToArray());
				inversionCount += inversions;

				(sortedValues, inversions) = MergeAndCount(leftValues, rightValues);
				inversionCount += inversions;

				return (sortedValues, inversionCount);
			}
		}

		private static (int[] mergedValues, long splitCount) MergeAndCount(int[] leftValues, int[] rightValues)
		{
			int[] mergedValues = new int[leftValues.Length + rightValues.Length];

			long i = 0, j = 0, k = 0, splitCount = 0;

			while (k < mergedValues.Length)
			{
				if (leftValues[i] < rightValues[j])
				{
					mergedValues[k] = leftValues[i];
					i++;
					k++;
				}
				else if (j <= rightValues.Length - 1)
				{
					mergedValues[k] = rightValues[j];
					j++;
					k++;
					splitCount += leftValues.Length - i;
				}

				if (i == leftValues.Length)
				{
					// if we've reached the end of leftArray, copy remaining items from right
					while (j < rightValues.Length)
					{
						mergedValues[k++] = rightValues[j++];
					}
				}
				else if (j == rightValues.Length)
				{
					// if we've reached the end of rightArray, copy remaining items from left
					while (i < leftValues.Length)
					{
						mergedValues[k++] = leftValues[i++];
					}
				}
			}



			return (mergedValues, splitCount);
		}

	}
}
