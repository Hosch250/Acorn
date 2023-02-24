import { Box, Tab, Tabs } from "@mui/material";
import axios from "axios";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { TabPanel } from "../components/TabPanel";
import { config } from "../config";
interface Category {
  id: string;
  name: string;
  description: string;
  isHomepage: boolean;
}
export function Category() {
  const [categories, setCategories] = useState<Category[]>([]);
  const { id } = useParams();

  const [activeTab, setActiveTab] = useState(id);

  useEffect(() => {
    // todo: move category api call to a context
    axios
      .get<Category[]>(config.baseUrl + "Category")
      .then((result) => {
        setCategories(result.data);

        if (!id) {
          setActiveTab(result.data[0].id);
        }
      })
      .catch(console.log);
  }, []);

  const handleChange = (_: React.SyntheticEvent, newValue: number) => {
    setActiveTab(categories[newValue]?.id);
  };

  function a11yProps(index: string) {
    return {
      id: `tab-${index}`,
      "aria-controls": `tabpanel-${index}`,
    };
  }
  return (
    <>
      {/* todo: loading view*/}
      {categories.length > 0 && (
        <Box sx={{ borderBottom: 1, borderColor: "divider" }}>
          <Tabs
            value={categories.findIndex((f) => f.id === activeTab)}
            onChange={handleChange}
            aria-label="categories"
          >
            {categories.map((m) => (
              <Tab key={m.id} label={m.name} {...a11yProps(m.id)} />
            ))}
          </Tabs>
        </Box>
      )}
      {categories.length > 0 &&
        categories.map((m) => (
          <TabPanel key={m.id} id={m.id} active={activeTab === m.id}>
            {m.description}
          </TabPanel>
        ))}
    </>
  );
}
