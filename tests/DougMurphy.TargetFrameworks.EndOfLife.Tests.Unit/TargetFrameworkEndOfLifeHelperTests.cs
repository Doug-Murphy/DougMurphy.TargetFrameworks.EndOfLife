using DougMurphy.TargetFrameworks.EndOfLife.Enums;
using DougMurphy.TargetFrameworks.EndOfLife.Exceptions;
using DougMurphy.TargetFrameworks.EndOfLife.Helpers;
using DougMurphy.TargetFrameworks.EndOfLife.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace DougMurphy.TargetFrameworks.EndOfLife.Tests.Unit;

[Parallelizable(ParallelScope.All)]
public class TargetFrameworkEndOfLifeHelperTests {
	[Test(Description = "When given a singular TFM that is currently EOL, it determines that it is EOL.")]
	public void TargetFrameworkThatIsEolCorrectlyShowsEol() {
		const string TFM_TO_USE = "net45";

		TargetFrameworkCheckResponse eolResults = TargetFrameworkEndOfLifeHelper.CheckTargetFrameworkForEndOfLife(TFM_TO_USE);

		CollectionAssert.IsNotEmpty(eolResults.EndOfLifeTargetFrameworks);
		CollectionAssert.Contains(eolResults.EndOfLifeTargetFrameworks, new KeyValuePair<string, DateTime>("net45", new DateTime(2016, 01, 12)));
	}

	[Test(Description = "When given a singular TFM that is currently EOL, and forecasting the date forward, it determines that it is EOL.")]
	public void TargetFrameworkThatIsEolCorrectlyShowsEolWithDayForecasting() {
		const string TFM_TO_USE = "net45";

		TargetFrameworkCheckResponse eolResults = TargetFrameworkEndOfLifeHelper.CheckTargetFrameworkForEndOfLife(TFM_TO_USE, TimeframeUnit.Day, 1);

		CollectionAssert.IsNotEmpty(eolResults.EndOfLifeTargetFrameworks);
		CollectionAssert.Contains(eolResults.EndOfLifeTargetFrameworks, new KeyValuePair<string, DateTime>("net45", new DateTime(2016, 01, 12)));
	}

	[Test(Description = "When given a singular TFM that is not EOL, it determines that it is not EOL.")]
	public void TargetFrameworkThatIsNotEolCorrectlyShowsNotEol() {
		const string TFM_TO_USE = "net6.0";

		TargetFrameworkCheckResponse eolResults = TargetFrameworkEndOfLifeHelper.CheckTargetFrameworkForEndOfLife(TFM_TO_USE);

		CollectionAssert.IsEmpty(eolResults.EndOfLifeTargetFrameworks);
	}

	[Test(Description = "When given two TFM's where both are EOL, it determines that they are both EOL.")]
	public void TwoTargetFrameworksWhenBothAreEolCorrectlyShowsEol() {
		const string TFM_TO_USE = "net45;netcoreapp2.1";

		TargetFrameworkCheckResponse eolResults = TargetFrameworkEndOfLifeHelper.CheckTargetFrameworkForEndOfLife(TFM_TO_USE);

		CollectionAssert.IsNotEmpty(eolResults.EndOfLifeTargetFrameworks);
		CollectionAssert.Contains(eolResults.EndOfLifeTargetFrameworks, new KeyValuePair<string, DateTime>("net45", new DateTime(2016, 01, 12)));
		CollectionAssert.Contains(eolResults.EndOfLifeTargetFrameworks, new KeyValuePair<string, DateTime>("netcoreapp2.1", new DateTime(2021, 08, 21)));
	}

	[Test(Description = "When given two TFM's where only one is EOL, it determines that the correct one is EOL.")]
	public void TwoTargetFrameworksWhenOneIsEolCorrectlyShowsEol() {
		const string TFM_TO_USE = "net45;net48";

		TargetFrameworkCheckResponse eolResults = TargetFrameworkEndOfLifeHelper.CheckTargetFrameworkForEndOfLife(TFM_TO_USE);

		CollectionAssert.IsNotEmpty(eolResults.EndOfLifeTargetFrameworks);
		CollectionAssert.Contains(eolResults.EndOfLifeTargetFrameworks, new KeyValuePair<string, DateTime>("net45", new DateTime(2016, 01, 12)));
		CollectionAssert.DoesNotContain(eolResults.EndOfLifeTargetFrameworks.Keys, "net48");
	}

	[Test(Description = "When given no TFM's, an exception is thrown.")]
	public void EmptyStringTargetFrameworkThrowsArgumentNullException() {
		const string TFM_TO_USE = "";

		Assert.Throws<ArgumentNullException>(() => TargetFrameworkEndOfLifeHelper.CheckTargetFrameworkForEndOfLife(TFM_TO_USE));
	}

	[Test(Description = "When given an empty multiple TFM, an exception is thrown.")]
	public void EmptyMultipleTargetFrameworksThrowsArgumentException() {
		const string TFM_TO_USE = ";";

		Assert.Throws<ArgumentException>(() => TargetFrameworkEndOfLifeHelper.CheckTargetFrameworkForEndOfLife(TFM_TO_USE));
	}

	[Test(Description = "When given a valid format TFM, but the TFM is not currently registered, an exception is thrown.")]
	public void UnregisteredTargetFrameworksThrowsArgumentException() {
		const string TFM_TO_USE = "net60";

		Assert.Throws<TargetFrameworkUnknownException>(() => TargetFrameworkEndOfLifeHelper.CheckTargetFrameworkForEndOfLife(TFM_TO_USE));
	}

	[Test(Description = "When getting all EOL TFM's not forecasting, the list is not empty.")]
	public void GettingAllEndOfLifeTargetFrameworksWithoutForecastingReturnsNonEmptyList() {
		TargetFrameworkCheckResponse allCurrentEolTfms = TargetFrameworkEndOfLifeHelper.GetAllEndOfLifeTargetFrameworkMonikers();

		CollectionAssert.IsNotEmpty(allCurrentEolTfms.EndOfLifeTargetFrameworks);
		//intentionally not asserting on number of TFMs since that is date-driven and mocking date isn't important enough for this to be honest
	}

	[Test(Description = "When getting all EOL TFM's with forecasting day, the list is not empty.")]
	public void GettingAllEndOfLifeTargetFrameworksWithDayForecastingReturnsNonEmptyList() {
		TargetFrameworkCheckResponse allCurrentEolTfms = TargetFrameworkEndOfLifeHelper.GetAllEndOfLifeTargetFrameworkMonikers(TimeframeUnit.Day, 1);

		CollectionAssert.IsNotEmpty(allCurrentEolTfms.EndOfLifeTargetFrameworks);
		//intentionally not asserting on number of TFMs since that is date-driven and mocking date isn't important enough for this to be honest
	}

	[Test(Description = "When getting all EOL TFM's with forecasting week, the list is not empty.")]
	public void GettingAllEndOfLifeTargetFrameworksWithWeekForecastingReturnsNonEmptyList() {
		TargetFrameworkCheckResponse allCurrentEolTfms = TargetFrameworkEndOfLifeHelper.GetAllEndOfLifeTargetFrameworkMonikers(TimeframeUnit.Week, 1);

		CollectionAssert.IsNotEmpty(allCurrentEolTfms.EndOfLifeTargetFrameworks);
		//intentionally not asserting on number of TFMs since that is date-driven and mocking date isn't important enough for this to be honest
	}

	[Test(Description = "When getting all EOL TFM's with forecasting month, the list is not empty.")]
	public void GettingAllEndOfLifeTargetFrameworksWithMonthForecastingReturnsNonEmptyList() {
		TargetFrameworkCheckResponse allCurrentEolTfms = TargetFrameworkEndOfLifeHelper.GetAllEndOfLifeTargetFrameworkMonikers(TimeframeUnit.Month, 1);

		CollectionAssert.IsNotEmpty(allCurrentEolTfms.EndOfLifeTargetFrameworks);
		//intentionally not asserting on number of TFMs since that is date-driven and mocking date isn't important enough for this to be honest
	}

	[Test(Description = "When getting all EOL TFM's with forecasting year, the list is not empty.")]
	public void GettingAllEndOfLifeTargetFrameworksWithYearForecastingReturnsNonEmptyList() {
		TargetFrameworkCheckResponse allCurrentEolTfms = TargetFrameworkEndOfLifeHelper.GetAllEndOfLifeTargetFrameworkMonikers(TimeframeUnit.Year, 1);

		CollectionAssert.IsNotEmpty(allCurrentEolTfms.EndOfLifeTargetFrameworks);
		//intentionally not asserting on number of TFMs since that is date-driven and mocking date isn't important enough for this to be honest
	}

	[Test(Description = "When getting all EOL TFM's with invalid forecasting configuration, an exception is thrown.")]
	public void GettingAllEndOfLifeTargetFrameworksWithInvalidForecastingThrowsException() {
		Assert.Throws<ArgumentOutOfRangeException>(() => TargetFrameworkEndOfLifeHelper.GetAllEndOfLifeTargetFrameworkMonikers((TimeframeUnit)5, 1));
	}
}
