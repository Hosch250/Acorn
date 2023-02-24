import { Box, Typography } from "@mui/material";

interface TabPanelProps {
  children?: React.ReactNode;
  id: string;
  active: boolean;
}

export function TabPanel({ children, id, active, ...other }: TabPanelProps) {
  return (
    <div
      role="tabpanel"
      hidden={!active}
      id={`tabpanel-${id}`}
      aria-labelledby={`tab-${id}`}
      {...other}
    >
      {active && (
        <Box sx={{ p: 3 }}>
          <Typography>{children}</Typography>
        </Box>
      )}
    </div>
  );
}
