# Get Started

Step 1: Custom Message Constants.

<code>
public enum Messages
{
    Alert
}
</code>
<br />
<br />

Step 2: Subscribe Message Event.

<code>
Messager.Subscribe(Messages.Alert, m => MessageBox.Show((string)m[0]));
</code>
<br />
<br />

Subscribe On UI Thread:

<code>
Messager.Subscribe(Messages.Alert, m => MessageBox.Show((string)m[0]), true);
</code>
<br />
<br />

Step 3: Publish Message Event.

<code>
Messager.Publish(Messages.Alert, "Hello");
</code>
