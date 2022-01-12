using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sort
{
    public static class QuickSort
    {
		public static int Sort(int[] values, PivotType pivotType)
		{
			return Sort(values, 0, values.Length - 1, pivotType);
		}

		private static int Sort(int[] values, int l, int r, PivotType pivotType)
		{
			if (l >= r) return 0;
			int comparisonCount = 0;
			int i = ChoosePivotIndex(values, pivotType, l, r);
			Swap(l, i, values);

			(int pivotPosition, int count) = Partition(values, l, r);
			comparisonCount += count;
			comparisonCount += Sort(values, l, pivotPosition - 1, pivotType);
			comparisonCount += Sort(values, pivotPosition + 1, r, pivotType);

			return comparisonCount;
		}

		private static (int pivotIndex, int comparisonCount) Partition(int[] values, int l, int r)
		{
			int pivot = values[l];
			int i = l + 1;
			int j = l + 1;
			int comparisonCount = r - l;

			while (j <= r)
			{
				if (values[j] < pivot)
				{
					Swap(i, j, values);
					i++;
				}
				j++;
			}
			int pivotIndex = i - 1;
			Swap(l, pivotIndex, values);
			return (pivotIndex, comparisonCount);
		}

		private static void Swap(int i, int j, int[] values)
		{
			if (i != j)
			{
				int temp = values[i];
				values[i] = values[j];
				values[j] = temp;
			}
		}

		private static int ChoosePivotIndex(int[] values, PivotType pivotType, int l, int r)
		{
			switch (pivotType)
			{
				case PivotType.First:
					return l;
				case PivotType.Last:
					return r;
				case PivotType.Median:
				default:
					int medianIndex = l + (r - l) / 2;

					List<int> a = new List<int> { values[l], values[medianIndex], values[r] };
					a.Sort();
					int medianValue = a[1];
					return Array.IndexOf(values, medianValue);
			}
		}
	}
}
