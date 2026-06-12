public void Test(string id)
{
    using var connection = new SqliteConnection(":memory:");

    // Безопасно: параметризованный запрос (Semgrep его не тронет)
    connection.Execute("SELECT * FROM customers WHERE id = 1");

    // 🚨 Опасно: прямая конкатенация строк (Semgrep обнаружит это)
    connection.Execute("SELECT * FROM customers WHERE id = '" + id + "'");

    // 🚨 Опасно: строковая интерполяция (тоже будет найдено)
    connection.Execute($"SELECT * FROM customers WHERE id = '{id}'");
}