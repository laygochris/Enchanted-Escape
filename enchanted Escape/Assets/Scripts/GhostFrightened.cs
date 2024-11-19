using UnityEngine;

public class GhostFrightened : GhostBehavior
{
    private bool eaten;
    [SerializeField] GameObject IceVfX;

    public override void Enable(float duration)
    {
        base.Enable(duration);

        GetComponent<Movement>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        IceVfX.SetActive(true);

        Invoke(nameof(Flash), duration / 2f);
    }

    public override void Disable()
    {
        base.Disable();
        GetComponent<Movement>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
        GetComponent<GhostChase>().enabled = false;
        GetComponent<Ghost>().GhostBody.color = Color.white;
        IceVfX.SetActive(false);
    }

    private void Eaten()
    {
        eaten = true;
        ghost.SetPosition(ghost.home.inside.position);
        ghost.home.Enable(duration);

        GetComponent<Movement>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
    }

    private void Flash()
    {
        if (!eaten)
        {
        }
    }

    private void OnEnable()
    {
        ghost.movement.speedMultiplier = 0.5f;
        eaten = false;
    }

    private void OnDisable()
    {
        ghost.movement.speedMultiplier = 1f;
        eaten = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && enabled)
        {
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;

            foreach (Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
                float distance = (ghost.target.position - newPosition).sqrMagnitude;

                if (distance > maxDistance)
                {
                    direction = availableDirection;
                    maxDistance = distance;
                }
            }

            ghost.movement.SetDirection(direction);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (enabled) {
                Eaten();
            }
        }
    }

}
