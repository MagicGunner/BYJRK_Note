using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using ValidationResult = System.Windows.Controls.ValidationResult;

namespace TextBoxValidateDemo;

public class MainWindowViewModel : ObservableObject, IDataErrorInfo {
    public string? UserName {
        get;
        set {
            // if (value?.Length is < 6 or > 10) {
            //     throw new ArgumentException("用户名不能小于6个字符且不能大于10个字符");
            // }

            if (SetProperty(ref field, value)) {
                OnPropertyChanged(nameof(Error));
            }
        }
    }


    public int Age {
        get;
        set {
            if (SetProperty(ref field, value)) {
                OnPropertyChanged(nameof(Error));
            }
        }
    }

    public string Error {
        get {
            var errors = new List<string> {
                                              this[nameof(UserName)],
                                              this[nameof(Age)]
                                          };
            return string.Join(Environment.NewLine, errors.Where(e => !string.IsNullOrEmpty(e)));
        }
    }

    public string this[string columnName] {
        get {
            if (columnName == nameof(UserName)) {
                if (string.IsNullOrWhiteSpace(UserName)) {
                    return "UserName不能为空";
                }

                if (UserName.Length < 6 || UserName.Length > 10) {
                    return "用户名不能小于6个字符且不能大于10个字符";
                }
            } else if (columnName == nameof(Age)) {
                if (Age is < 18 or > 120) {
                    return "年龄必须在18到120之间";
                }
            }

            return string.Empty;
        }
    }
}

public class AnotherViewModel : ObservableObject, INotifyDataErrorInfo {
    public string? UserName {
        get;
        set {
            // if (value?.Length is < 6 or > 10) {
            //     throw new ArgumentException("用户名不能小于6个字符且不能大于10个字符");
            // }

            SetProperty(ref field, value);
        }
    }


    public int Age {
        get;
        set {
            SetProperty(ref field, value);
        }
    }

    public IEnumerable GetErrors(string? propertyName) {
        throw new NotImplementedException();
    }

    public bool                                            HasErrors { get; }
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
}

public class OtherViewModel : ObservableValidator {
    [Required(ErrorMessage = "UserName不能为空")]
    [MinLength(6, ErrorMessage = "用户名不能小于6个字符")]
    [MaxLength(10, ErrorMessage = "用户名不能大于10个字符")]
    public string? UserName {
        get;
        set {
            // if (value?.Length is < 6 or > 10) {
            //     throw new ArgumentException("用户名不能小于6个字符且不能大于10个字符");
            // }

            SetProperty(ref field, value, true);
        }
    }


    [Required(ErrorMessage = "Age不能为空")]
    [Range(18, 120, ErrorMessage = "年龄必须在18到120之间")]
    public int Age {
        get;
        set {
            SetProperty(ref field, value, true);
        }
    }
}

class StringLengthRule : ValidationRule {
    public int? MaxLength { get; set; }
    public int? MinLength { get; set; }


    public override ValidationResult Validate(object? value, CultureInfo cultureInfo) {
        if (value is not string text) {
            return new ValidationResult(false, "Value must be a string");
        }

        if (MinLength.HasValue && text.Length < MinLength.Value) {
            return new ValidationResult(false, $"字符串长度不能小于{MinLength.Value}");
        }

        if (MaxLength.HasValue && text.Length > MaxLength.Value) {
            return new ValidationResult(false, $"字符串长度不能大于{MaxLength.Value}");
        }

        return ValidationResult.ValidResult;
    }
}