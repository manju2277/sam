﻿namespace Smartstore.Core.Common.Services
{
    public partial interface IRoundingHelper
    {
        /// <summary>Rounds an amount.</summary>
        /// <param name="amount">Amount to round.</param>
        /// <param name="currency">
        /// Rounds <paramref name="amount"/> using <see cref="Currency.RoundNumDecimals"/>.
        /// If <c>null</c>, currency will be obtained via <see cref="IWorkContext.WorkingCurrency"/>.
        /// </param>
        /// <returns>Rounded amount.</returns>
        /// <remarks>This is a gereral method to round currency values.</remarks>
        decimal Round(decimal amount, Currency currency = null);

        /// <summary>Rounds an amount.</summary>
        /// <param name="amount">Amount to round.</param>
        /// <param name="reason">The reason to round <paramref name="amount"/>.</param>
        /// <param name="isTax">A value indicating whether <paramref name="amount"/> is a tax amount.</param>
        /// <param name="currency">
        /// Rounds <paramref name="amount"/> according to its <see cref="Currency.RoundCartRule"/>.
        /// If <c>null</c>, currency will be obtained via <see cref="IWorkContext.WorkingCurrency"/>.
        /// </param>
        /// <returns>Rounded amount.</returns>
        /// <remarks>This method is only intended to be used during shopping cart calculation.</remarks>
        decimal Round(decimal amount, RoundingReason reason, bool isTax, Currency currency = null);

        /// <summary>
        /// Round amount up or down to the nearest multiple of denomination (cash rounding) if activated for currency.
        /// </summary>
        /// <param name="amount">Amount to round.</param>
        /// <param name="toNearestRounding">Amount by which was rounded.</param>
        /// <param name="currency">
        /// Currency. Rounding must be activated for this currency.
        /// If <c>null</c>, currency will be obtained via <see cref="IWorkContext.WorkingCurrency"/>.
        /// </param>
        /// <returns>Rounded amount.</returns>
        /// <example>"Schweizer Rappenrundung" of 16.23 -> returned value is 16.25 and toNearestRounding is 0.02.</example>
        /// <remarks>Usually this method is used to round the order total.</remarks>
        decimal RoundToNearest(decimal amount, out decimal toNearestRounding, Currency currency = null);
    }
}
