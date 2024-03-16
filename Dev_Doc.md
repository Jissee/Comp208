# Dev. Doc.
1. 所有的命名空间`NameSpace`（文件夹）均按照大驼峰法。
2. 所有的类名`class`均遵循大驼峰命名规范。
3. 所有的接口`interface`以`Capital I`为前缀使用大驼峰法
4. 所有的方法名`method`均遵循大驼峰命名规范。
5. 如果需要使用泛型`generic`，泛型类型采用以下命名方式：
   5.1 一般类型使用`<T>`   
   5.2 键值类型使用`<K, V>`
6. 所有的变量名`variable`需要区分不同情况：
   6.1 对于类中的变量来说，如果此变量读写访问权限相同，按照公共/私有字段处理。
   6.2 对于类中的变量来说，如果只读`Read Only`，按照公共属性处理
   6.3 对于类中的变量来说，如果字段赋值需要特殊检查（eg. 检查范围、预处理）按照公共属性处理。这种情况下必须定义一个字段，否则会造成栈溢出错误。
   6.4 对于函数参数和局部变量来说，按照小驼峰法命名。
7. 对于所有的字段`Fields`，全部按照小驼峰法。
8. 对于所有的属性`Properties`，全部按照大驼峰法。

## Example: 
1. 命名空间`Namespcae`
   ```csharp
    namespace EoE.Server {}
   ```
2. 类名`Class`
   ```csharp
    class ServerPlayer {}
   
    class Encoder<T> {}
   ```
3. 接口`Interface`
   ```csharp
    public interface IPacket<T> {}
    public interface IBasePacket {}
   ```
4. 方法`Method`
   ```csharp
    public void SendPacket<T>(T packet) where T: IPacket<T> 
    {
        private int playerId;
    }
    public void Tick() {}
   ```
5. 泛型`Generic`见其他例子
6. 变量名及属性`Variable and Propeties`
   6.1
   ```csharp
    public string playerName;
   ```
   6.2
   ```csharp
    public int Population {get:}
   ```
   6.3
   ```csharp
    private int tradeItem;
    public int TradeItem 
    {
        get => tradeItem: 
        set 
        {
            if(condition)
            {
                tradeItem = value; //value 是一个关键字
            }
        }
    }
    ```
    6.4 见第四部分
7. 见第六部分


