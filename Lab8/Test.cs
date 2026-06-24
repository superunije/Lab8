namespace Lab8.Models
{
    public record class Test(
        int ID, 
        string Title, 
        string Subject, 
        int QuestionCount, 
        int MaxScore, 
        bool IsTimed)
    {
        public static Test Deserialize(BinaryReader reader)
        {
            return new(
                reader.ReadInt32(),
                reader.ReadString(),
                reader.ReadString(),
                reader.ReadInt32(),
                reader.ReadInt32(),
                reader.ReadBoolean());
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(ID);
            writer.Write(Title);
            writer.Write(Subject);
            writer.Write(QuestionCount);
            writer.Write(MaxScore);
            writer.Write(IsTimed);
        }

        public override string ToString()
        {
            return $"[{ID}] {Title} | {Subject} | Вопросов: {QuestionCount}, Балл: {MaxScore}, Таймер: {IsTimed}";
        }
    }
}
