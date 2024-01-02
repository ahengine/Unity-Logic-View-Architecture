namespace UScreens
{
    public static class RouterBase
    {
        public static UScreen Current { private set; get; } = null;

        public static void ChangeState(UScreen screen)
        {
            if(Current == screen)
                return;

            if(Current != null) Current.Hide();
            Current = screen;
            if (Current != null) Current.Show();
        }
    }
}
