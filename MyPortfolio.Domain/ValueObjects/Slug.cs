using MyPortfolio.Domain.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyPortfolio.Domain.ValueObjects
{
    public sealed record Slug
    {
        public string Value { get; }

        private Slug(string value)
        {
            Value = value;
        }

        public static Slug Create(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Slug không được rỗng.");

            var slug = title.ToLowerInvariant().Trim();

            slug = RemoveDiacritics(slug);

            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");
            slug = Regex.Replace(slug, @"\s+", "-");
            slug = Regex.Replace(slug, @"-+", "-");
            slug = slug.Trim('-');

            if (string.IsNullOrWhiteSpace(slug))
                throw new DomainException("Slug không hợp lệ.");

            return new Slug(slug);
        }

        private static string RemoveDiacritics(string text)
        {
            var normalized = text.Normalize(NormalizationForm.FormD);

            var sb = new StringBuilder();

            foreach (var c in normalized)
            {
                var category =
                    CharUnicodeInfo.GetUnicodeCategory(c);

                if (category != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            return sb.ToString()
                .Normalize(NormalizationForm.FormC)
                .Replace('đ', 'd')
                .Replace('Đ', 'D');
        }

        public override string ToString() => Value;

        public static implicit operator string(Slug slug)
            => slug.Value;
    }

    //public sealed class Slug
    //{
    //    public string Value { get; }
    //    private Slug(string value) => Value = value;

    //    public static Slug Create(string title)
    //    {
    //        if (string.IsNullOrWhiteSpace(title))
    //            throw new DomainException("Slug không được rỗng.");

    //        var slug = title.ToLowerInvariant().Trim();

    //        // Xử lý tiếng Việt có dấu
    //        slug = RemoveDiacritics(slug);

    //        // Chỉ giữ chữ thường, số, dấu gạch ngang
    //        slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");
    //        slug = Regex.Replace(slug, @"\s+", "-");
    //        slug = Regex.Replace(slug, @"-+", "-");
    //        slug = slug.Trim('-');

    //        if (string.IsNullOrEmpty(slug))
    //            throw new DomainException("Slug không hợp lệ sau khi xử lý.");

    //        if (slug.Length > 200)
    //            throw new DomainException("Slug quá dài.");

    //        return new Slug(slug);
    //    }

    //    private static string RemoveDiacritics(string text)
    //    {
    //        // Map ký tự tiếng Việt sang không dấu
    //        var map = new Dictionary<string, string>
    //    {
    //        {"à|á|ả|ã|ạ|ă|ắ|ặ|ằ|ẳ|ẵ|â|ấ|ầ|ẩ|ẫ|ậ", "a"},
    //        {"è|é|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ", "e"},
    //        {"ì|í|ỉ|ĩ|ị", "i"},
    //        {"ò|ó|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ", "o"},
    //        {"ù|ú|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự", "u"},
    //        {"ỳ|ý|ỷ|ỹ|ỵ", "y"},
    //        {"đ", "d"}
    //    };

    //        foreach (var entry in map)
    //            foreach (var c in entry.Key.Split('|'))
    //                text = text.Replace(c, entry.Value);

    //        return text;
    //    }

    //    public override string ToString() => Value;
    //    public override bool Equals(object? obj) => obj is Slug other && Value == other.Value;
    //    public override int GetHashCode() => Value.GetHashCode();
    //}

}

